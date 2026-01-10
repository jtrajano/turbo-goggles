import { Badge } from "@/components/ui/badge";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { MoreHorizontal } from "lucide-react";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";

const orders = [
  {
    id: "#12345",
    customer: "John Smith",
    avatar: "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=32&h=32&fit=crop&crop=face",
    email: "john@example.com",
    product: "Premium Plan",
    amount: "$299.00",
    status: "completed",
    date: "Jan 20, 2024",
  },
  {
    id: "#12346",
    customer: "Sarah Johnson",
    avatar: "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=32&h=32&fit=crop&crop=face",
    email: "sarah@example.com",
    product: "Basic Plan",
    amount: "$99.00",
    status: "pending",
    date: "Jan 19, 2024",
  },
  {
    id: "#12347",
    customer: "Mike Chen",
    avatar: "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=32&h=32&fit=crop&crop=face",
    email: "mike@example.com",
    product: "Enterprise Plan",
    amount: "$599.00",
    status: "completed",
    date: "Jan 18, 2024",
  },
  {
    id: "#12348",
    customer: "Emily Davis",
    avatar: "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=32&h=32&fit=crop&crop=face",
    email: "emily@example.com",
    product: "Premium Plan",
    amount: "$299.00",
    status: "cancelled",
    date: "Jan 17, 2024",
  },
  {
    id: "#12349",
    customer: "Alex Thompson",
    avatar: "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=32&h=32&fit=crop&crop=face",
    email: "alex@example.com",
    product: "Basic Plan",
    amount: "$99.00",
    status: "processing",
    date: "Jan 16, 2024",
  },
];

const statusStyles: Record<string, string> = {
  completed: "bg-success/10 text-success hover:bg-success/20",
  pending: "bg-warning/10 text-warning hover:bg-warning/20",
  processing: "bg-primary/10 text-primary hover:bg-primary/20",
  cancelled: "bg-destructive/10 text-destructive hover:bg-destructive/20",
};

export function OrdersTable() {
  return (
    <div className="bg-card rounded-xl border animate-fade-in">
      <div className="p-6 border-b">
        <h3 className="text-lg font-semibold">Recent Orders</h3>
        <p className="text-sm text-muted-foreground">Manage your latest transactions</p>
      </div>
      <div className="overflow-x-auto">
        <table className="w-full">
          <thead>
            <tr className="border-b bg-muted/50">
              <th className="text-left text-xs font-medium text-muted-foreground uppercase tracking-wider py-3 px-6">
                Order
              </th>
              <th className="text-left text-xs font-medium text-muted-foreground uppercase tracking-wider py-3 px-6">
                Customer
              </th>
              <th className="text-left text-xs font-medium text-muted-foreground uppercase tracking-wider py-3 px-6 hidden md:table-cell">
                Product
              </th>
              <th className="text-left text-xs font-medium text-muted-foreground uppercase tracking-wider py-3 px-6">
                Amount
              </th>
              <th className="text-left text-xs font-medium text-muted-foreground uppercase tracking-wider py-3 px-6 hidden sm:table-cell">
                Status
              </th>
              <th className="text-right text-xs font-medium text-muted-foreground uppercase tracking-wider py-3 px-6">
                Actions
              </th>
            </tr>
          </thead>
          <tbody>
            {orders.map((order) => (
              <tr key={order.id} className="border-b last:border-0 hover:bg-muted/30 transition-colors">
                <td className="py-4 px-6">
                  <span className="font-medium">{order.id}</span>
                  <p className="text-xs text-muted-foreground sm:hidden mt-0.5">
                    {order.date}
                  </p>
                </td>
                <td className="py-4 px-6">
                  <div className="flex items-center gap-3">
                    <Avatar className="h-8 w-8">
                      <AvatarImage src={order.avatar} />
                      <AvatarFallback>{order.customer.split(" ").map(n => n[0]).join("")}</AvatarFallback>
                    </Avatar>
                    <div className="hidden sm:block">
                      <p className="font-medium text-sm">{order.customer}</p>
                      <p className="text-xs text-muted-foreground">{order.email}</p>
                    </div>
                    <span className="sm:hidden font-medium text-sm">{order.customer.split(" ")[0]}</span>
                  </div>
                </td>
                <td className="py-4 px-6 hidden md:table-cell">
                  <span className="text-sm">{order.product}</span>
                </td>
                <td className="py-4 px-6">
                  <span className="font-semibold">{order.amount}</span>
                </td>
                <td className="py-4 px-6 hidden sm:table-cell">
                  <Badge variant="secondary" className={statusStyles[order.status]}>
                    {order.status}
                  </Badge>
                </td>
                <td className="py-4 px-6 text-right">
                  <DropdownMenu>
                    <DropdownMenuTrigger asChild>
                      <button className="p-2 hover:bg-secondary rounded-lg">
                        <MoreHorizontal className="h-4 w-4" />
                      </button>
                    </DropdownMenuTrigger>
                    <DropdownMenuContent align="end">
                      <DropdownMenuItem>View Details</DropdownMenuItem>
                      <DropdownMenuItem>Edit Order</DropdownMenuItem>
                      <DropdownMenuItem className="text-destructive">Cancel</DropdownMenuItem>
                    </DropdownMenuContent>
                  </DropdownMenu>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
