using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Core.Entities;

namespace WeatherAggregator.Core.Logic
{
    public static class EmailComposer
    {
        private static string domainName = @"http://averageweather.apphb.com";
        private static string confirmSubscriptionApi = @"/api/Subscription/ConfirmSubscription";
        private static string unsobscribeApi = @"/api/Subscription/Unsubscribe";
        private static string mailFrom = "weatheraggregatortest@gmail.com";
        private static string ConfirmSubscriptionSubject = "Запрос на подтверждение подписки";
        private static string UpdateSubscriptionSubject = "Запрос на обновление подписки";
        private static string SubscriptionConfirmedSubject = "Подписка подтверждена";
        private static string UnsubscriptionSubject = "Подписка отменена";
        private static string NotificationSubject = "Оповещение о дожде";
       


        public static MailMessage GetConfirmSubscriptionMail(SubscriptionResponse subscription)
        {
            return subscription.IsUpdating
                ? GetUpdateSubscriptionMail(subscription)
                : GetNewSubscriptionMail(subscription);
        }


        private static MailMessage GetNewSubscriptionMail(SubscriptionResponse subscription)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailFrom);
            mail.To.Add(new MailAddress(subscription.Email));
            mail.IsBodyHtml = true;
            mail.Subject = ConfirmSubscriptionSubject;
            mail.IsBodyHtml = true;
            mail.Body = string.Format(
                "Вы подписались в сервисе «Погодный агрегатор» и после подтверждения подписки вы будете получать сообщения о прогнозе дождя в месте {0}.<br>" +
                "Чтобы подтвердить подписку, нажмите на <a href=\"{1}{2}?key={3}\">эту ссылку</a>. <br>" +
                "Если вы не подписывались, просто проигнорируйте это письмо",
                subscription.AddressText, domainName, confirmSubscriptionApi, subscription.Key);
            return mail;
        }

        private static MailMessage GetUpdateSubscriptionMail(SubscriptionResponse subscription)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailFrom);
            mail.To.Add(new MailAddress(subscription.Email));
            mail.IsBodyHtml = true;
            mail.Subject = ConfirmSubscriptionSubject;
            mail.IsBodyHtml = true;
            mail.Body = string.Format(
                "Вы обновляете адресс для отследивания дождей. Теперь это: {0}.<br>" +
                "Чтобы подтвердить это обновление, нажмите на <a href=\"{1}{2}?key={3}\">эту ссылку</a>. <br>" +
                "Если вы не не меняли аддресс, просто проигнорируйте это письмо",
                subscription.AddressText, domainName, confirmSubscriptionApi, subscription.Key);
            return mail;
        }

        public static MailMessage GetSubscriptionConfirmedMail(SubscriptionResponse subscription)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailFrom);
            mail.To.Add(new MailAddress(subscription.Email));
            mail.IsBodyHtml = true;
            mail.Subject = SubscriptionConfirmedSubject;
            mail.IsBodyHtml = true;
            mail.Body = string.Format(
                "Подписка подтверждена. Теперь вы будете получать уведомления когда дождь будет собираться идти в: {0}.<br>" +
                "Если вы хотите отписаться от уведомлений, нажмите на <a href=\"{1}{2}?key={3}\">эту ссылку</a>. <br>",
                subscription.AddressText, domainName, unsobscribeApi, subscription.Key);
            return mail;
        }

        public static MailMessage GetUnsubscribenMail(string email)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailFrom);
            mail.To.Add(new MailAddress(email));
            mail.IsBodyHtml = true;
            mail.Subject = UnsubscriptionSubject;
            mail.IsBodyHtml = true;
            mail.Body = string.Format(
                "Подписка на «Погодный агрегатор» отменена<br>" +
                "Тёплого рождества вам!");
            return mail;
        }

        public static MailMessage GetNotificationMail(SubscriptionInfo subscription, List<DateTime> rainDates)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailFrom);
            mail.To.Add(new MailAddress(subscription.Email));
            mail.IsBodyHtml = true;
            mail.Subject = NotificationSubject;
            mail.IsBodyHtml = true;
            string rainDatesMessage = string.Join(", ", rainDates);
            mail.Body = string.Format(
                "В ближайшее время ожидается дождь. <br>" +
                "Вот даты, года он крайне вероятен:{0}<br>" +
                "Если вы хотите отписаться от уведомлений, нажмите на <a href=\"{1}{2}?key={3}\">эту ссылку</a>. "
                , rainDatesMessage, domainName, unsobscribeApi, subscription.Key);
            return mail;
            
        }
    }

}
