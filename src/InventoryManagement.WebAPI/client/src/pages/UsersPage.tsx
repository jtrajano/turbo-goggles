
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Search, Plus, MoreHorizontal, Mail, Phone, Loader2 } from "lucide-react";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { useState } from "react";
import { Alert, AlertDescription } from "@/components/ui/alert";
import { useUsers, useDeleteUser, useUsersPage } from "@/hooks/useUsers";
import { useToast } from "@/hooks/use-toast";
import Counter from "@/components/Counter";
import { useDeleteUserMutation, useGetUsersQuery } from "@/services/usersApi";
import PagingBar from "@/components/PagingBar";

const roleStyles: Record<string, string> = {
  Admin: "bg-primary/10 text-primary",
  Editor: "bg-warning/10 text-warning",
  Viewer: "bg-muted text-muted-foreground",
};

const statusStyles: Record<string, string> = {
  active: "bg-success/10 text-success",
  inactive: "bg-muted text-muted-foreground",
  pending: "bg-warning/10 text-warning",
};


const ITEMS_PER_PAGE = 6;

export default function UsersPage() {
  const [searchQuery, setSearchQuery] = useState("");
  const [currentPage, setCurrentPage] = useState(1);

  const { data: pagedResult, isLoading, error, isError } =
    useGetUsersQuery({ filter: searchQuery, pageNumber: currentPage, pageSize: ITEMS_PER_PAGE});
  
  // Delete mutation
  const [deleteUser] = useDeleteUserMutation();
  const handleDeleteUser = async(id: string)=>{
    if(!confirm('Are you sure?')) return;
    try {
      await deleteUser(id).unwrap();
      toast({title: 'User deleted'});
    } catch (err) {
      toast({
        title: 'Delete failed',
        description: (err).message, 
        variant: 'destructive'
      });
    }
  }

  const paginatedUsers = pagedResult?.items || [];
  const totalPages = Math.ceil((pagedResult?.totalCount || 0) / ITEMS_PER_PAGE);
  const { toast } = useToast();
  
  return (
      <div className="space-y-6">
        {/* Header */}
        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
          <div>
            <h1 className="text-2xl font-bold tracking-tight">Users</h1>
            <p className="text-muted-foreground">Manage your team members and their roles</p>
          </div>
          <Button className="w-full sm:w-auto">
            <Plus className="h-4 w-4 mr-2" />
            Add User
          </Button>
        </div>

        {/* Search */}
        <div className="relative max-w-md">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-muted-foreground" />
          <Input 
            placeholder="Search users..." 
            className="pl-10"
            value={searchQuery}
            onChange={(e) => {
              setSearchQuery(e.target.value);
              setCurrentPage(1);
            }}
          />
        </div>

        {/* Error Alert */}
        {isError && (
          <Alert variant="destructive">
            <AlertDescription>
              {error instanceof Error ? error.message : "Failed to fetch users"}
            </AlertDescription>
          </Alert>
        )}

        {/* Loading State */}
        {isLoading && (
          <div className="flex items-center justify-center py-12">
            <Loader2 className="h-8 w-8 animate-spin text-muted-foreground" />
          </div>
        )}

        {/* Empty State */}
        {!isLoading && !isError && paginatedUsers.length === 0 && (
          <div className="text-center py-12">
            <p className="text-muted-foreground">
              {searchQuery ? "No users found matching your search." : "No users found."}
            </p>
          </div>
        )}

        {/* Users Grid */}
        {!isLoading && !isError && paginatedUsers.length > 0 && (
          <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
            {paginatedUsers.map((user) => (
            <div
              key={user.id}
              className="bg-card rounded-xl border p-6 card-hover animate-fade-in"
            >
              <div className="flex items-start justify-between">
                <div className="flex items-center gap-4">
                  <Avatar className="h-12 w-12">
                    <AvatarImage src={user.avatar} />
                    <AvatarFallback>{user.name.split(" ").map(n => n[0]).join("")}</AvatarFallback>
                  </Avatar>
                  <div>
                    <h3 className="font-semibold">{user.name}</h3>
                    <p className="text-sm text-muted-foreground">{user.email}</p>
                  </div>
                </div>
                <DropdownMenu>
                  <DropdownMenuTrigger asChild>
                    <button className="p-2 hover:bg-secondary rounded-lg">
                      <MoreHorizontal className="h-4 w-4" />
                    </button>
                  </DropdownMenuTrigger>
                  <DropdownMenuContent align="end">
                    <DropdownMenuItem>View Profile</DropdownMenuItem>
                    <DropdownMenuItem>Edit User</DropdownMenuItem>
                    <DropdownMenuItem 
                      className="text-destructive"
                      onClick={() => handleDeleteUser(user.id)}
                    >
                      Remove
                    </DropdownMenuItem>
                  </DropdownMenuContent>
                </DropdownMenu>
              </div>

              <div className="mt-4 flex items-center gap-2">
                <Badge variant="secondary" className={roleStyles[user.role]}>
                  {user.role}
                </Badge>
                <Badge variant="secondary" className={statusStyles[user.status]}>
                  {user.status}
                </Badge>
              </div>

              <div className="mt-4 pt-4 border-t flex items-center justify-between">
                <span className="text-xs text-muted-foreground">
                  Active: {user.lastActive}
                </span>
                <div className="flex gap-1">
                  <button className="p-2 hover:bg-secondary rounded-lg">
                    <Mail className="h-4 w-4 text-muted-foreground" />
                  </button>
                  <button className="p-2 hover:bg-secondary rounded-lg">
                    <Phone className="h-4 w-4 text-muted-foreground" />
                  </button>
                </div>
              </div>
            </div>
            ))}
          </div>
        )}

        {/* Pagination */}
        
          <PagingBar
            currentPage={currentPage}
            itemsPerPage={ITEMS_PER_PAGE}
            totalCount={pagedResult?.totalCount} 
            totalPages={totalPages}
            setCurrentPage={setCurrentPage}  
          />
          
        
     </div>


   
  );
}
