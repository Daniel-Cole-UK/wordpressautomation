A black-box automated testing framework concept for the Wordpress dashboard written in Visual C# and utilising Selenium WebDriver.

To run:

- Amend the baseURL property in \WordpressAutomation\Selenium\Driver.cs to point to a local or online wordpress instance. E.g "http://localhost/wordpress/"
- Set your username and password in \WordpressAutomation\WordpressTests\Base Classes\WordpressTests.cs (defaults are username:"admin", password:"admin").