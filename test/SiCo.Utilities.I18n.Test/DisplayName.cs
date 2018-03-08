//namespace SiCo.Utilities.I18n.Test
//{
//    using System;
//    using Xunit;

//    public class DisplayName
//    {
//        [Theory]
//        [InlineData(typeof(I18n.Enums.DbLogAction))]
//        [InlineData(typeof(I18n.Enums.DbLogStatus))]
//        [InlineData(typeof(Web.Enums.PasswordChangeReason))]
//        [InlineData(typeof(Web.Enums.TransactionCodes))]
//        public void Enum(Type enumCheck)
//        {
//            var info = Generics.EnumExtensions.GetFullInfo(enumCheck);

//            Assert.NotNull(info);
//            Assert.NotEmpty(info);
//        }

//        [Theory]
//        [InlineData(typeof(Web.Models.SignInModel))]
//        [InlineData(typeof(Web.Models.PasswordChangeModel))]
//        [InlineData(typeof(Web.Models.PasswordForgetModel))]
//        [InlineData(typeof(Web.Models.TransactionModel))]
//        [InlineData(typeof(Web.Models.TransactionsListModel))]
//        [InlineData(typeof(Web.Models.Common.BaseModel))]
//        [InlineData(typeof(Web.Models.Common.DeleteModel))]
//        [InlineData(typeof(Web.Models.Common.DetailModel))]
//        [InlineData(typeof(Web.Models.Common.EnableModel))]
//        public void Model(Type modelCheck)
//        {
//            Common.CheckDisplayAttributes(modelCheck);
//        }
//    }
//}