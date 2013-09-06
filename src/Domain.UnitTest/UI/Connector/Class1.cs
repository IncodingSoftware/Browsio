//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Browsio.UnitTest.UI.Connector
//{
//    using System.Web.Mvc;
//    using Browsio.Controllers;
//    using Browsio.Domain;
//    using Incoding.Block.IoC;
//    using Incoding.MSpecContrib;
//    using Machine.Specifications;



//    [Subject(typeof(ConnectorController))]
//    public class When_ConnectorController_Success_Is_Exist_Facebook_User
//    {
//        #region Establish value

//        static MockController<ConnectorController> mockController;

//        static ActionResult result;

//        static ConnectorOfType type;

//        #endregion

//        Establish establish = () =>
//                                  {
//                                      type = Pleasure.Generator.Enum<ConnectorOfType>();

//                                      IOAuthProvider oAuthProvider = Pleasure.MockStrictAsObject<IOAuthProvider>(mock => mock.Setup(r => r.GetAuthorizeToken(Pleasure.Generator.Invent<Dictionary<string, string>>()))
//                                                                                                                             .Returns(Pleasure.Generator.String));

//                                      string accessToken = IoCFactory.Instance.StubTryResolveByNamed<IOAuthProvider>(type.ToString(), oAuthProvider)
//                                      mockController = MockController<ConnectorController>
//                                              .When();
//                                  };

//        Because of = () => { result = mockController.Original.Verb; };


//        It should_be_success = () => result.ShouldBeIncodingSuccess();

//        It should_be_push = () => mockController.ShouldBePush(input);
//    }
//}
