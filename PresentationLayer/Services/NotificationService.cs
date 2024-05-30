using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.Interface.IService;
using DomainLayer.Models;
using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

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

        public async Task<ICollection<NotificationViewModel>> GetNotifications(int employeeId)
        {
            var notifications = await _unitOfWork.Context.Notification
                .Where(n => n.EmployeeId == employeeId)
                .ToListAsync();

            return _mapper.Map<ICollection<NotificationViewModel>>(notifications);
        }

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