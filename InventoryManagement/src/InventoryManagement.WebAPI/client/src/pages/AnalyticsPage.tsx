import { AdminLayout } from "@/components/layout/AdminLayout";
import { StatCard } from "@/components/dashboard/StatCard";
import { Eye, Clock, MousePointerClick, Users } from "lucide-react";
import {
  AreaChart,
  Area,
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
  PieChart,
  Pie,
  Cell,
} from "recharts";

const pageViews = [
  { name: "Mon", views: 4000 },
  { name: "Tue", views: 3000 },
  { name: "Wed", views: 5000 },
  { name: "Thu", views: 2780 },
  { name: "Fri", views: 1890 },
  { name: "Sat", views: 2390 },
  { name: "Sun", views: 3490 },
];

const browserData = [
  { name: "Chrome", value: 45 },
  { name: "Safari", value: 25 },
  { name: "Firefox", value: 15 },
  { name: "Edge", value: 10 },
  { name: "Other", value: 5 },
];

const topPages = [
  { page: "/dashboard", views: 12453, bounce: "32%" },
  { page: "/users", views: 8234, bounce: "28%" },
  { page: "/settings", views: 5632, bounce: "45%" },
  { page: "/analytics", views: 4521, bounce: "22%" },
  { page: "/orders", views: 3245, bounce: "38%" },
];

const COLORS = ["hsl(238, 84%, 67%)", "hsl(142, 76%, 36%)", "hsl(38, 92%, 50%)", "hsl(0, 84%, 60%)", "hsl(220, 9%, 46%)"];

export default function AnalyticsPage() {
  return (
    <AdminLayout>
      <div className="space-y-6">
        {/* Header */}
        <div>
          <h1 className="text-2xl font-bold tracking-tight">Analytics</h1>
          <p className="text-muted-foreground">Track your website performance and user behavior</p>
        </div>

        {/* Stats */}
        <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
          <StatCard
            title="Page Views"
            value="128,430"
            change="+12.3%"
            changeType="positive"
            icon={Eye}
          />
          <StatCard
            title="Avg. Session"
            value="4m 32s"
            change="+8.1%"
            changeType="positive"
            icon={Clock}
          />
          <StatCard
            title="Bounce Rate"
            value="34.2%"
            change="-2.4%"
            changeType="positive"
            icon={MousePointerClick}
          />
          <StatCard
            title="Active Users"
            value="1,234"
            change="+15.2%"
            changeType="positive"
            icon={Users}
          />
        </div>

        {/* Charts Row */}
        <div className="grid gap-6 lg:grid-cols-2">
          {/* Page Views Chart */}
          <div className="bg-card rounded-xl border p-6 animate-fade-in">
            <h3 className="text-lg font-semibold mb-4">Page Views</h3>
            <div className="h-64">
              <ResponsiveContainer width="100%" height="100%">
                <BarChart data={pageViews}>
                  <CartesianGrid strokeDasharray="3 3" stroke="hsl(220, 13%, 91%)" vertical={false} />
                  <XAxis dataKey="name" axisLine={false} tickLine={false} tick={{ fill: "hsl(220, 9%, 46%)", fontSize: 12 }} />
                  <YAxis axisLine={false} tickLine={false} tick={{ fill: "hsl(220, 9%, 46%)", fontSize: 12 }} />
                  <Tooltip
                    contentStyle={{
                      backgroundColor: "hsl(0, 0%, 100%)",
                      border: "1px solid hsl(220, 13%, 91%)",
                      borderRadius: "8px",
                    }}
                  />
                  <Bar dataKey="views" fill="hsl(238, 84%, 67%)" radius={[4, 4, 0, 0]} />
                </BarChart>
              </ResponsiveContainer>
            </div>
          </div>

          {/* Browser Distribution */}
          <div className="bg-card rounded-xl border p-6 animate-fade-in">
            <h3 className="text-lg font-semibold mb-4">Browser Distribution</h3>
            <div className="h-64 flex items-center justify-center">
              <ResponsiveContainer width="100%" height="100%">
                <PieChart>
                  <Pie
                    data={browserData}
                    cx="50%"
                    cy="50%"
                    innerRadius={60}
                    outerRadius={80}
                    paddingAngle={5}
                    dataKey="value"
                  >
                    {browserData.map((_, index) => (
                      <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                    ))}
                  </Pie>
                  <Tooltip
                    contentStyle={{
                      backgroundColor: "hsl(0, 0%, 100%)",
                      border: "1px solid hsl(220, 13%, 91%)",
                      borderRadius: "8px",
                    }}
                    formatter={(value: number) => [`${value}%`, "Share"]}
                  />
                </PieChart>
              </ResponsiveContainer>
            </div>
            <div className="mt-4 grid grid-cols-2 gap-2">
              {browserData.map((item, index) => (
                <div key={item.name} className="flex items-center gap-2">
                  <div className="h-3 w-3 rounded-full" style={{ backgroundColor: COLORS[index] }} />
                  <span className="text-sm text-muted-foreground">{item.name} ({item.value}%)</span>
                </div>
              ))}
            </div>
          </div>
        </div>

        {/* Top Pages */}
        <div className="bg-card rounded-xl border animate-fade-in">
          <div className="p-6 border-b">
            <h3 className="text-lg font-semibold">Top Pages</h3>
            <p className="text-sm text-muted-foreground">Most visited pages this month</p>
          </div>
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead>
                <tr className="border-b bg-muted/50">
                  <th className="text-left text-xs font-medium text-muted-foreground uppercase tracking-wider py-3 px-6">
                    Page
                  </th>
                  <th className="text-left text-xs font-medium text-muted-foreground uppercase tracking-wider py-3 px-6">
                    Views
                  </th>
                  <th className="text-left text-xs font-medium text-muted-foreground uppercase tracking-wider py-3 px-6">
                    Bounce Rate
                  </th>
                </tr>
              </thead>
              <tbody>
                {topPages.map((page) => (
                  <tr key={page.page} className="border-b last:border-0 hover:bg-muted/30 transition-colors">
                    <td className="py-4 px-6">
                      <span className="font-medium text-primary">{page.page}</span>
                    </td>
                    <td className="py-4 px-6">
                      <span>{page.views.toLocaleString()}</span>
                    </td>
                    <td className="py-4 px-6">
                      <span className="text-muted-foreground">{page.bounce}</span>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </AdminLayout>
  );
}
