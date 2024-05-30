using DomainLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interface.IService
{
    public interface INotificationService
    {
        Task AddNotification(int employeeId, string message);
        Task<ICollection<NotificationViewModel>> GetNotifications(int employeeId);
        Task MarkAsRead(int notificationId);
    }
}
