using BNPL_Web.Notification.Models;

namespace BNPL_Web.Notification.Interface
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
}
