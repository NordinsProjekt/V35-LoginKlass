using LoginClass;
namespace Unittest_Login
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DataRow("admin@uddevalla.se","Password123!")]
        public void TestLoginUser_WithValidEmailAndPassword_ShouldReturnTrue(string email, string password)
        {
            var login = new Login();
            bool result = login.LoginUser(email, password);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("admin@uddevalla.se", "paSsword\"34")]
        [DataRow("admin@uddevall.se", "Password123!")]
        public void TestLoginUser_WithValidEmailAndPassword_ShouldReturnFalse(string email, string password)
        {
            var login = new Login();
            bool result = login.LoginUser(email, password);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("admin@uddevalla.co", "Password123!")]
        [DataRow("admin@uddevalla.com2", "Password123!")]
        [DataRow("admin@uddevalla.re", "Password123!")]
        [DataRow(".admin@uddevallase", "Password123!")]
        [DataRow("adminuddevalla.com", "Password123!")]
        [DataRow("adewqrdfr32r23r32r23n@uddevalla.com", "Password123!")]
        public void TestLoginUser_WithInValidEmailAndPassword_ShouldReturnExceptionForEmail(string email, string password)
        {
            var login = new Login();
            var ex = Assert.ThrowsException<ArgumentException>(() => login.LoginUser(email, password));
            Assert.AreEqual("Email is not valid",ex.Message);
        }

        [TestMethod]
        #region(Test Data)
        [DataRow("admin@uddevalla.se", "pass")]
        [DataRow("admin@uddevalla.se", "pass5t46567543")]
        [DataRow("admin@uddevalla.se", "Passe324r3")]
        [DataRow("admin@uddevalla.se", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [DataRow("admin@uddevalla.se", "!!!!!!!!")]
        [DataRow("admin@uddevalla.se", "a!!!!!!!!!!9")]
        #endregion
        public void TestLoginUser_WithInValidEmailAndPassword_ShouldReturnExceptionForPassword(string email, string password)
        {
            var login = new Login();
            var ex = Assert.ThrowsException<ArgumentException>(() => login.LoginUser(email, password));
            Assert.AreEqual("Password is not valid", ex.Message);
        }
    }
}