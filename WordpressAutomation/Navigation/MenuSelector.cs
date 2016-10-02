using OpenQA.Selenium;

namespace WordpressAutomation
{
    // Class for easy selection of left navigation buttons and submenus
    public class MenuSelector
    {
        public static void Select(string topLevelMenuId, string subMenuLinkText)
        {
            Driver.Instance.FindElement(By.Id(topLevelMenuId)).Click();
            Driver.Instance.FindElement(By.LinkText(subMenuLinkText)).Click();
        }
    }
}
