namespace Browsio.Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding.Block.IoC;
    using Incoding.CQRS;
    using Incoding.Utilities;

    #endregion

    ////ncrunch: no coverage start
    public class UserSetUp : ISetUp
    {
        #region Fields

        readonly IDispatcher dispatcher;

        #endregion

        #region Constructors

        public UserSetUp(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        #endregion

        #region ISetUp Members

        public int GetOrder()
        {
            return 1;
        }

        public void Execute()
        {
            if (this.dispatcher.Query(new GetEntitiesQuery<User>()).Any())
                return;

            var emailSender = IoCFactory.Instance.TryResolve<IEmailSender>();
            IoCFactory.Instance.Forward(new FakeEmailSender(message => { }));
            this.dispatcher.Push(new AddUserCommand{Login = "admin@mail.com", Password = "password", });
            IoCFactory.Instance.Forward(emailSender);

            this.dispatcher.Push(new ConfirUserCommand
                                     {
                                             UserId = this.dispatcher.Query(new GetEntitiesQuery<User>()).First()
                                                          .Id.ToString()
                                     });
        }

        #endregion

        #region Disposable

        public void Dispose() { }

        #endregion
    }

    ////ncrunch: no coverage end
}