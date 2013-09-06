namespace Browsio.Contrib
{
    #region << Using >>

    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using Browsio.Domain;
    using Incoding.Extensions;
    using Incoding.MetaLanguageContrib;
    using Incoding.MvcContrib;

    #endregion

    ////ncrunch: no coverage start
    public class BrowsioHtmlHelper<TModel>
    {
        #region Fields

        readonly HtmlHelper<TModel> htmlHelper;

        #endregion

        #region Constructors

        public BrowsioHtmlHelper(HtmlHelper<TModel> htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }

        #endregion

        #region Api Methods

        public MvcHtmlString OpenDialog(Action<SettingOpenDialog> action, object attributes = null)
        {
            var setting = new SettingOpenDialog();
            action(setting);

            return OpenFormDialog(setting.Url, setting.Bind, GlobalSelector.DefaultDialogId, attributes)
                    .ToLink(setting.Content);
        }

        public RouteValueDictionary OpenFormDialog(string url, JqueryBind typeEvent = JqueryBind.Click, string DialogId = GlobalSelector.DefaultDialogId, object attributes = null)
        {
            var htmlAttributes = this.htmlHelper
                                     .When(typeEvent)
                                     .DoWithPreventDefault()
                                     .AjaxGet(url)
                                     .OnSuccess(dsl =>
                                                    {
                                                        dsl.With(selector => selector.Id(DialogId)).Behaviors(dslin =>
                                                                                                                  {
                                                                                                                      dslin.Core().Insert.Html();
                                                                                                                      dslin.JqueryUI().Dialog.Open(builder =>
                                                                                                                                                       {
                                                                                                                                                           builder.Modal = true;
                                                                                                                                                           builder.Width = "auto";
                                                                                                                                                           builder.Resizable = false;
                                                                                                                                                           builder.Draggable = false;
                                                                                                                                                       });
                                                                                                                  });
                                                        dsl.With(Selector.Jquery.Class("ui-widget-overlay")).Core().JQuery.Css.Set("z-index", "1800");
                                                        dsl.With(Selector.Jquery.Class("ui-dialog")).Core().JQuery.Css.Set("z-index", "2000");
                                                    })
                                     .AsHtmlAttributes(attributes);
            return htmlAttributes;
        }

        public RouteValueDictionary DialogClose(JqueryBind typeEvent, string DialogId, object attributes = null)
        {
            var htmlAttributes = this.htmlHelper
                                     .When(typeEvent)
                                     .DoWithPreventDefault()
                                     .Direct()
                                     .OnSuccess(dsl => dsl.With(selector => selector.Id(DialogId)).JqueryUI().Dialog.Close())
                                     .AsHtmlAttributes(attributes);
            return htmlAttributes;
        }

        public MvcHtmlString ButtonDialogClose()
        {
            return DialogClose(JqueryBind.Click, GlobalSelector.DefaultDialogId, new { @class = "btn btn-warning" })
                    .ToButton("Cancel");
        }

        public MvcForm BeginForm(Action<BeginFormSetting> action, object attributes = null)
        {
            var setting = new BeginFormSetting();
            action(setting);

            var attr = AnonymousHelper.ToDictionary(attributes);
            if (!attr.ContainsKey("class"))
                attr.Set("class", "form-horizontal");
            attr.Set("action", this.htmlHelper.ViewContext.HttpContext.Request.RawUrl);

            if (setting.IsUploadForm)
                attr.Set("enctype", "multipart/form-data");

            var meta = this.htmlHelper
                           .When(JqueryBind.Submit)
                           .DoWithPreventDefaultAndStopPropagation()
                           .Submit()
                           .OnBegin(dsl => dsl.Self().JqueryPlugIn().Block.Lock(options => { }))
                           .OnError(dsl => dsl.Self().Core().Form.Validation.Refresh())
                           .OnSuccess(dsl =>
                                          {
                                              if (setting.CloseDialog)
                                                  dsl.With(selector => selector.Id(GlobalSelector.DefaultDialogId)).JqueryUI().Dialog.Close();

                                              if (setting.ReloadSubmit)
                                              {
                                                  dsl.With(selector => selector.Id(GlobalSelector.DefaultDialogId)).Core().Insert.Html();
                                                  dsl.With(selector => selector.Id(GlobalSelector.DefaultDialogId)).Browsio().CenterDialog();
                                              }

                                              if (!string.IsNullOrWhiteSpace(setting.NotificationMessage))
                                                  dsl.Self().JqueryPlugIn().Notification.PinesNoty.Show(options => { options.Text = setting.NotificationMessage; });

                                              if (setting.RedirectToSelf)
                                                  dsl.Utilities.Document.RedirectToSelf();

                                              if (setting.IsBack)
                                                  dsl.Utilities.Document.HistoryGo(-1);
                                          })
                           .OnComplete(dsl => dsl.Self().JqueryPlugIn().Block.UnLock())
                           .AsHtmlAttributes(attr);

            return this.htmlHelper.BeginForm(setting.Action, setting.Controller, FormMethod.Post, meta);
        }

        public MvcHtmlString Load(Action<LoadSetting> action)
        {
            var setting = new LoadSetting();
            action(setting);

            return this.htmlHelper.When(JqueryBind.InitIncoding | JqueryBind.None)
                       .Do()
                       .AjaxGet(setting.Url)
                       .OnBegin(dsl =>
                                    {
                                        if (setting.IsBlock)
                                            dsl.Self().JqueryPlugIn().Block.Lock();
                                    })
                       .OnSuccess(dsl =>
                                      {
                                          if (setting.IsAppend)
                                              dsl.Self().Core().Insert.WithTemplate(setting.Template).Append();
                                          else
                                              dsl.Self().Core().Insert.WithTemplate(setting.Template).Html();
                                      })
                       .OnComplete(dsl =>
                                       {
                                           if (setting.IsBlock)
                                               dsl.Self().JqueryPlugIn().Block.UnLock();
                                       })
                       .AsHtmlAttributes(new { id = setting.Id })
                       .ToDiv();
        }

        public MvcHtmlString ButtonVerb(Action<ButtonVerbSetting> action)
        {
            var setting = new ButtonVerbSetting();
            action(setting);

            return this.htmlHelper.When(JqueryBind.Click)
                       .Do()
                       .AjaxPost(HttpUtility.UrlDecode(setting.Url))
                       .OnSuccess(dsl =>
                                      {
                                          if (!string.IsNullOrWhiteSpace(setting.Notification))
                                              dsl.Self().JqueryPlugIn().Notification.PinesNoty.Show(options => { options.Text = setting.Notification; });

                                          if (setting.RedirectToSelf)
                                              dsl.Utilities.Document.RedirectToSelf();
                                      })
                       .AsHtmlAttributes(new { @class = "btn btn-primary" })
                       .ToButton(setting.Content);
        }

        public MvcHtmlString ButtonBack()
        {
            return this.htmlHelper.When(JqueryBind.Click)
                       .Do()
                       .Direct()
                       .OnSuccess(dsl => dsl.Utilities.Document.HistoryGo(-1))
                       .AsHtmlAttributes(new { @class = "btn bar-warning" })
                       .ToLink("Back");
        }

        #endregion

        #region Nested classes

        public class BeginFormSetting
        {
            #region Properties

            public bool CloseDialog { get; set; }

            public bool ReloadSubmit { get; set; }

            public string Action { get; set; }

            public string Controller { get; set; }

            public string NotificationMessage { get; set; }

            public bool IsUploadForm { get; set; }

            public bool IsBack { get; set; }

            public bool RedirectToSelf { get; set; }

            #endregion
        }

        public class SettingOpenDialog
        {
            #region Constructors

            public SettingOpenDialog()
            {
                Bind = JqueryBind.Click;
                Modal = true;
            }

            #endregion

            #region Properties

            public string Url { get; set; }

            public string Content { get; set; }

            public JqueryBind Bind { get; set; }

            public bool Modal { get; set; }

            #endregion
        }

        public class LoadSetting
        {
            #region Constructors

            public LoadSetting()
            {
                Id = Guid.NewGuid().ToString();
            }

            #endregion

            #region Properties

            public string Id { get; set; }

            public string Url { get; set; }

            public Selector Template { get; set; }

            public bool IsBlock { get; set; }

            public bool IsAppend { get; set; }

            #endregion
        }

        public class ButtonVerbSetting
        {
            #region Properties

            public string Url { get; set; }

            public string Notification { get; set; }

            public string Content { get; set; }

            public bool RedirectToSelf { get; set; }

            #endregion
        }

        #endregion
    }

    ////ncrunch: no coverage end
}