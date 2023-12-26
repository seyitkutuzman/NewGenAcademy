﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using System.Diagnostics;
using OpenQA.Selenium.DevTools.V118.WebAuthn;

namespace NewGenAcademy
{
    internal class Program
    {

        static IWebDriver driver = new ChromeDriver();
        static string userCode = "2169";
        static string password = "Talisca94.";
        static int status = 0;
        static bool progress = false;
        static string firstProgressStatus = null;
        static string lastProgressStatus = null;
        static string[] newWindow = null;
        static void Main(string[] args)
        {
            //System.Console.WriteLine("Please Enter Your User Code: ");
            //userCode = System.Console.ReadLine();
            //System.Console.WriteLine("Please Enter Your Password: ");
            //password = System.Console.ReadLine();
            driver.Navigate().GoToUrl("https://akademi.eximbank.gov.tr/eximakademi/eep/login/?&err=-1");
            driver.Manage().Window.Maximize();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1000));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div/div[3]/span/input")));
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div/div[3]/span/input")).SendKeys(userCode);
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div/div[4]/span/input")).SendKeys(password);
            driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div[2]/div/div/div/button")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='dynamicContent']/div[2]/div/a")));
            driver.Navigate().GoToUrl("https://akademi.eximbank.gov.tr/eximakademi/eep/main/catalog/74?objectId=74");
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"dynamicContent\"]/div/div/div[2]/div/div[16]/div/a")));
            while (status == 0)
            {
                try
                {
                    if (!(driver.FindElement(By.XPath("//*[@id=\"dynamicContent\"]/div/div/div[2]/div/div[16]/div/div/div[1]/div[2]/div[1]")).Text == "Tamamladın"))
                    {
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"dynamicContent\"]/div/div/div[2]/div/div[16]/div/a")));
                        driver.FindElement(By.XPath("//*[@id=\"dynamicContent\"]/div/div/div[2]/div/div[16]/div/a")).Click();
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"activity-unit-detail\"]/div/div[2]/div/div/div[1]/button")));
                        //Store the ID of the original window
                        string originalWindow = driver.CurrentWindowHandle;

                        //Check we don't have other windows open already
                        Debug.Equals(driver.WindowHandles.Count, 1);

                        driver.FindElement(By.XPath("//*[@id=\"activity-unit-detail\"]/div/div[2]/div/div/div[1]/button")).Click();

                        //Wait for the new window or tab
                        wait.Until(wd => wd.WindowHandles.Count == 2);

                        //Loop through until we find a new window handle
                        foreach (string window in driver.WindowHandles)
                        {
                            if (originalWindow != window)
                            {
                                driver.SwitchTo().Window(window);
                                break;
                            }
                        }
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"progressTextDiv\"]")));
                        if (driver.FindElement(By.XPath("//*[@id=\"gotoLastLocationBtn\"]/div[2]")).Enabled)
                        {
                            driver.FindElement(By.XPath("//*[@id=\"gotoLastLocationBtn\"]/div[2]")).Click();
                            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"progressTextDiv\"]")));
                            firstProgressStatus = driver.FindElement(By.XPath("//*[@id=\"progressTextDiv\"]")).Text;
                            //while (!(driver.FindElement(By.XPath("//*[@id=\"pageHolderImgReplayBSOD4_replay_btn\"]")).Enabled))
                            //{
                            //    Thread.Sleep(10);
                            //}
                            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"pageHolderImgReplayBSOD4_replay_btn\"]")));
                            driver.FindElement(By.XPath("//*[@id=\"nextShine\"]")).Click();
                            System.Console.WriteLine(firstProgressStatus);
                            driver.Quit();
                        }
                        else
                        {
                            driver.FindElement(By.XPath("//*[@id=\"gotoLastLocationBtn\"]/div[2]")).Click();
                            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"progressTextDiv\"]")));
                            firstProgressStatus = driver.FindElement(By.XPath("//*[@id=\"progressTextDiv\"]")).Text;
                            //while (!(driver.FindElement(By.XPath("//*[@id=\"pageHolderImgReplayBSOD4_replay_btn\"]")).Enabled))
                            //{
                            //    Thread.Sleep(10);
                            //}
                            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"pageHolderImgReplayBSOD4_replay_btn\"]")));
                            driver.FindElement(By.XPath("//*[@id=\"nextShine\"]")).Click();
                            System.Console.WriteLine(firstProgressStatus);
                            driver.Quit();
                        }
                    }
                }
                catch
                {
                    break;
                }
                status++;
            }

            //*[@id="dynamicContent"]/div/div/div[2]/div/div[16]/div/a
            //*[@id="dynamicContent"]/div/div/div[2]/div/div[2]
            //*[@id="dynamicContent"]/div/div/div[2]/div/div[3]
            //*[@id="dynamicContent"]/div/div/div[2]/div/div[4]
            //*[@id="dynamicContent"]/div/div/div[2]/div/div[5]
            //*[@id="dynamicContent"]/div/div/div[2]/div/div[6]
            //*[@id="dynamicContent"]/div/div/div[2]/div/div[20]/div/div/div[1]/div[2]/div[1]/span
            //*[@id="dynamicContent"]/div/div/div[2]/div/div[16]/div/div/div[1]/div[2]/div[1]/span[1]


        }
    }
}
// başla //*[@id="activity-unit-detail"]/div/div[2]/div/div/div[1]/button
// progress text //*[@id="progressTextDiv"]
// next slide //*[@id="nextShine"]
// //*[@id="videoProgressPlayedDiv"]