using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.Interface.IService;
using DomainLayer.Models;
using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Provides service methods for managing notifications.
    /// </summary>
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for data access operations.</param>
        /// <param name="mapper">The mapper for object-object mapping.</param>
        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a notification for a specific employee.
        /// </summary>
        /// <param name="employeeId">The identifier of the employee.</param>
        /// <param name="message">The notification message.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddNotification(int employeeId, string message)
        {
            var notification = new Notification
            {
                EmployeeId = employeeId,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _unitOfWork.Context.Notification.Add(notification);
            await _unitOfWork.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves notifications for a specific employee.
        /// </summary>
        /// <param name="employeeId">The identifier of the employee.</param>
        /// <returns>A collection of notification view models.</returns>
        public async Task<ICollection<NotificationViewModel>> GetNotifications(int employeeId)
        {
            var notifications = await _unitOfWork.Context.Notification
                .Where(n => n.EmployeeId == employeeId)
                .ToListAsync();

            return _mapper.Map<ICollection<NotificationViewModel>>(notifications);
        }

        /// <summary>
        /// Marks a notification as read.
        /// </summary>
        /// <param name="notificationId">The identifier of the notification to mark as read.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task MarkAsRead(int notificationId)
        {
            var notification = await _unitOfWork.Context.Notification.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _unitOfWork.Context.SaveChangesAsync();
            }
        }
    }
}
