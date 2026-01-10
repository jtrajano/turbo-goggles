import { useState } from "react";
import { AdminLayout } from "@/components/layout/AdminLayout";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";
import { Search, Package } from "lucide-react";

interface Product {
  id: number;
  name: string;
  description: string;
  sku: string;
  price: number;
  quantity: number;
  image: string;
}

// Mock data for products
const mockProducts: Product[] = Array.from({ length: 48 }, (_, i) => ({
  id: i + 1,
  name: [
    "Wireless Bluetooth Headphones",
    "Ergonomic Office Chair",
    "Smart Watch Pro",
    "Portable Power Bank",
    "USB-C Hub Adapter",
    "Mechanical Keyboard",
    "4K Webcam",
    "Noise Canceling Earbuds",
    "LED Desk Lamp",
    "Laptop Stand",
    "External SSD 1TB",
    "Wireless Mouse",
  ][i % 12],
  description: [
    "Premium sound quality with 40-hour battery life and comfortable over-ear design.",
    "Fully adjustable with lumbar support, breathable mesh back, and armrests.",
    "Track fitness, receive notifications, and monitor health metrics all day.",
    "20,000mAh capacity with fast charging support for all devices.",
    "7-in-1 hub with HDMI, USB-A, SD card reader, and PD charging.",
    "RGB backlit keys with hot-swappable switches and aluminum frame.",
    "Ultra HD resolution with auto-focus and built-in noise-canceling mic.",
    "True wireless with ANC, transparency mode, and 8-hour playback.",
    "Adjustable brightness and color temperature with USB charging port.",
    "Aluminum construction with adjustable height and angle for ergonomics.",
    "NVMe speeds up to 1050MB/s with shock-resistant design.",
    "Silent clicks with precision tracking and long battery life.",
  ][i % 12],
  sku: `SKU-${String(i + 1).padStart(5, "0")}`,
  price: [79.99, 299.99, 249.99, 49.99, 39.99, 129.99, 89.99, 149.99, 34.99, 59.99, 119.99, 29.99][i % 12],
  quantity: Math.floor(Math.random() * 200) + 1,
  image: `https://images.unsplash.com/photo-${[
    "1505740420928-5e560c06d30e",
    "1580480055273-228ff5388ef8",
    "1523275335684-37898b6baf30",
    "1609091839311-d5365f9ff1c5",
    "1625723044792-44de16ccb4e9",
    "1587829741301-dc798b83add3",
    "1516035069371-29a1b244cc32",
    "1590658268037-6bf12165a8df",
    "1507003211169-0a1dd7228f2d",
    "1527864550417-7fd91fc51a46",
    "1531492746076-161ca9f3f3cd",
    "1527814050087-3793815479db",
  ][i % 12]}?w=400&h=400&fit=crop`,
}));

const ITEMS_PER_PAGE = 8;

export default function ProductsPage() {
  const [searchQuery, setSearchQuery] = useState("");
  const [currentPage, setCurrentPage] = useState(1);

  const filteredProducts = mockProducts.filter(
    (product) =>
      product.name.toLowerCase().includes(searchQuery.toLowerCase()) ||
      product.sku.toLowerCase().includes(searchQuery.toLowerCase()) ||
      product.description.toLowerCase().includes(searchQuery.toLowerCase())
  );

  const totalPages = Math.ceil(filteredProducts.length / ITEMS_PER_PAGE);
  const startIndex = (currentPage - 1) * ITEMS_PER_PAGE;
  const paginatedProducts = filteredProducts.slice(startIndex, startIndex + ITEMS_PER_PAGE);

  const handlePageChange = (page: number) => {
    if (page >= 1 && page <= totalPages) {
      setCurrentPage(page);
    }
  };

  const getPageNumbers = () => {
    const pages: (number | string)[] = [];
    if (totalPages <= 5) {
      for (let i = 1; i <= totalPages; i++) pages.push(i);
    } else {
      if (currentPage <= 3) {
        pages.push(1, 2, 3, 4, "...", totalPages);
      } else if (currentPage >= totalPages - 2) {
        pages.push(1, "...", totalPages - 3, totalPages - 2, totalPages - 1, totalPages);
      } else {
        pages.push(1, "...", currentPage - 1, currentPage, currentPage + 1, "...", totalPages);
      }
    }
    return pages;
  };

  const getStockStatus = (quantity: number) => {
    if (quantity === 0) return { label: "Out of Stock", variant: "destructive" as const };
    if (quantity < 20) return { label: "Low Stock", variant: "secondary" as const };
    return { label: "In Stock", variant: "default" as const };
  };

  return (
    <AdminLayout>
      <div className="space-y-6">
        {/* Header */}
        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
          <div>
            <h1 className="text-2xl font-bold text-foreground">Products</h1>
            <p className="text-muted-foreground">
              Manage your product inventory ({filteredProducts.length} products)
            </p>
          </div>
          <Button className="gap-2">
            <Package className="h-4 w-4" />
            Add Product
          </Button>
        </div>

        {/* Search */}
        <div className="relative max-w-md">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-muted-foreground" />
          <Input
            placeholder="Search products by name, SKU, or description..."
            value={searchQuery}
            onChange={(e) => {
              setSearchQuery(e.target.value);
              setCurrentPage(1);
            }}
            className="pl-10"
          />
        </div>

        {/* Products Grid */}
        {paginatedProducts.length > 0 ? (
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 xl:grid-cols-6 gap-3">
            {paginatedProducts.map((product) => {
              const stockStatus = getStockStatus(product.quantity);
              return (
                <Card key={product.id} className="card-hover overflow-hidden">
                  <div className="aspect-[4/3] relative bg-muted">
                    <img
                      src={product.image}
                      alt={product.name}
                      className="w-full h-full object-cover"
                      loading="lazy"
                    />
                    <Badge
                      variant={stockStatus.variant}
                      className="absolute top-1 right-1 text-[10px] px-1.5 py-0.5"
                    >
                      {stockStatus.label}
                    </Badge>
                  </div>
                  <CardContent className="p-2 space-y-1">
                    <div>
                      <h3 className="font-medium text-xs text-foreground line-clamp-1">
                        {product.name}
                      </h3>
                      <p className="text-[10px] text-muted-foreground font-mono">
                        {product.sku}
                      </p>
                    </div>
                    <p className="text-[10px] text-muted-foreground line-clamp-1">
                      {product.description}
                    </p>
                    <div className="flex items-center justify-between pt-1 border-t border-border">
                      <span className="text-sm font-bold text-foreground">
                        ${product.price.toFixed(2)}
                      </span>
                      <span className="text-[10px] text-muted-foreground">
                        Qty: {product.quantity}
                      </span>
                    </div>
                  </CardContent>
                </Card>
              );
            })}
          </div>
        ) : (
          <div className="text-center py-12">
            <Package className="h-12 w-12 text-muted-foreground mx-auto mb-4" />
            <h3 className="text-lg font-medium text-foreground">No products found</h3>
            <p className="text-muted-foreground">
              Try adjusting your search query
            </p>
          </div>
        )}

        {/* Pagination */}
        {totalPages > 1 && (
          <div className="flex flex-col sm:flex-row items-center justify-between gap-4 pt-4">
            <p className="text-sm text-muted-foreground">
              Showing {startIndex + 1}-{Math.min(startIndex + ITEMS_PER_PAGE, filteredProducts.length)} of{" "}
              {filteredProducts.length} products
            </p>
            <Pagination>
              <PaginationContent>
                <PaginationItem>
                  <PaginationPrevious
                    onClick={() => handlePageChange(currentPage - 1)}
                    className={currentPage === 1 ? "pointer-events-none opacity-50" : "cursor-pointer"}
                  />
                </PaginationItem>
                {getPageNumbers().map((page, index) => (
                  <PaginationItem key={index} className="hidden sm:block">
                    {page === "..." ? (
                      <span className="px-3 py-2">...</span>
                    ) : (
                      <PaginationLink
                        onClick={() => handlePageChange(page as number)}
                        isActive={currentPage === page}
                        className="cursor-pointer"
                      >
                        {page}
                      </PaginationLink>
                    )}
                  </PaginationItem>
                ))}
                <PaginationItem className="sm:hidden">
                  <span className="px-3 py-2 text-sm">
                    {currentPage} / {totalPages}
                  </span>
                </PaginationItem>
                <PaginationItem>
                  <PaginationNext
                    onClick={() => handlePageChange(currentPage + 1)}
                    className={currentPage === totalPages ? "pointer-events-none opacity-50" : "cursor-pointer"}
                  />
                </PaginationItem>
              </PaginationContent>
            </Pagination>
          </div>
        )}
      </div>
    </AdminLayout>
  );
}
