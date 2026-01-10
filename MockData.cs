using API.Domain.Entities;
using System.Data;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace API;



public class MockData
{
    public static IReadOnlyList<User> GetUsers( string? name)
    {

        var users =  new List<User>
        {
            new User {
                Id = 1,
                Name = "Sarah Chen",
                Email = "sarah@example.com",
                Role = "Admin",
                Status = "active",
                LastActive = "2 minutes ago",
                Avatar = "https=//images.unsplash.com/photo-1494790108377-be9c29b29330?w=64&h=64&fit=crop&crop=face",
            },
            new User
            {
                Id = 2,
                Name = "Mike Johnson",
                Email = "mike@example.com",
                Role = "Editor",
                Status = "active",
                LastActive = "1 hour ago",
                Avatar = "https=//images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=64&h=64&fit=crop&crop=face",
            },
            new User
            {
                Id = 3,
                Name = "Emily Davis",
                Email = "emily@example.com",
                Role = "Viewer",
                Status = "inactive",
                LastActive = "2 days ago",
                Avatar = "https=//images.unsplash.com/photo-1438761681033-6461ffad8d80?w=64&h=64&fit=crop&crop=face",
            },
            new User
            {
                Id = 4,
                Name = "Alex Thompson",
                Email = "alex@example.com",
                Role = "Editor",
                Status = "active",
                LastActive = "30 minutes ago",
                Avatar = "https=//images.unsplash.com/photo-1500648767791-00dcc994a43e?w=64&h=64&fit=crop&crop=face",
            },
            new User
            {
                Id = 5,
                Name = "Lisa Wang",
                Email = "lisa@example.com",
                Role = "Admin",
                Status = "active",
                LastActive = "Just now",
                Avatar = "https=//images.unsplash.com/photo-1534528741775-53994a69daeb?w=64&h=64&fit=crop&crop=face",
            },
            new User
            {
                Id = 6,
                Name = "James Wilson",
                Email = "james@example.com",
                Role = "Viewer",
                Status = "pending",
                LastActive = "Never",
                Avatar = "https=//images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=64&h=64&fit=crop&crop=face",
            }
        };

        if(!string.IsNullOrEmpty(name))
        {
            var filtered = users.Where(u => u.Name.Contains(name, StringComparison.OrdinalIgnoreCase) || u.Email.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return filtered;
        }

        return users;
    }
}