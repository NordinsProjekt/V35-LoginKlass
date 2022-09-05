using GlobalChat;
namespace Unittest_GlobalChat
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckIfLastMessageWasDeleted_ListShouldStartWithMessageNr2()
        {
            var chat = new Chat();
            for (int i = 0; i < 51; i++)
            {
                string user = "User" + i;
                string msg = "This is message nr " + (i+1);
                chat.Post(user, msg);
            }
            chat.Post("Markus", "Test0001");
            var tempMsg = chat.ShowMessage(0);
            Assert.AreEqual(tempMsg.message, "This is message nr 2");
        }

        [TestMethod]
        public void CheckIfLastMessageWasDeleted_ListShouldHave51Messages()
        {
            var chat = new Chat();
            for (int i = 0; i < 51; i++)
            {
                string user = "User" + i;
                string msg = "This is message nr " + (i + 1);
                chat.Post(user, msg);
            }
            chat.Post("Markus", "Test0001");
            var tempMsg = chat.GetAllMessages();
            Assert.AreEqual(51, tempMsg.Count);
        }
    }
}