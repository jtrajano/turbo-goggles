import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";

const activities = [
  {
    id: 1,
    user: "Sarah Chen",
    action: "created a new project",
    target: "Marketing Campaign",
    time: "2 minutes ago",
    avatar: "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=32&h=32&fit=crop&crop=face",
  },
  {
    id: 2,
    user: "Mike Johnson",
    action: "completed order",
    target: "#12345",
    time: "15 minutes ago",
    avatar: "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=32&h=32&fit=crop&crop=face",
  },
  {
    id: 3,
    user: "Emily Davis",
    action: "updated user permissions for",
    target: "Admin Team",
    time: "1 hour ago",
    avatar: "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=32&h=32&fit=crop&crop=face",
  },
  {
    id: 4,
    user: "Alex Thompson",
    action: "submitted a report for",
    target: "Q4 Analytics",
    time: "2 hours ago",
    avatar: "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=32&h=32&fit=crop&crop=face",
  },
  {
    id: 5,
    user: "Lisa Wang",
    action: "added a new team member to",
    target: "Development",
    time: "3 hours ago",
    avatar: "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=32&h=32&fit=crop&crop=face",
  },
];

export function RecentActivity() {
  return (
    <div className="bg-card rounded-xl border p-6 animate-fade-in">
      <h3 className="text-lg font-semibold mb-4">Recent Activity</h3>
      <div className="space-y-4">
        {activities.map((activity) => (
          <div key={activity.id} className="flex items-start gap-3">
            <Avatar className="h-9 w-9">
              <AvatarImage src={activity.avatar} />
              <AvatarFallback>{activity.user.split(" ").map(n => n[0]).join("")}</AvatarFallback>
            </Avatar>
            <div className="flex-1 min-w-0">
              <p className="text-sm">
                <span className="font-medium">{activity.user}</span>{" "}
                <span className="text-muted-foreground">{activity.action}</span>{" "}
                <span className="font-medium text-primary">{activity.target}</span>
              </p>
              <p className="text-xs text-muted-foreground mt-0.5">{activity.time}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
