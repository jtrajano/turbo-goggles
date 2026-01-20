import { useEffect, useState } from "react";
 
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Search, Package } from "lucide-react";
import PagingBar from "@/components/PagingBar";
import { useGetProductsQuery } from "@/services/productsApi";


// Mock data for products

export default function ProductsPage() {
  const [searchQuery, setSearchQuery] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const [debouncedSearch, setDebouncedSearch] = useState(searchQuery);
  const ITEMS_PER_PAGE = 8;

  const { data: pagedResult, isLoading, error, isError } = 
    useGetProductsQuery({filter: debouncedSearch,pageNumber: currentPage, pageSize: ITEMS_PER_PAGE});

  const paginatedProducts = pagedResult?.items || [];

    // debounce search input to avoid excessive API calls
  useEffect(() => {
    const id = setTimeout(() => setDebouncedSearch(searchQuery), 300);
    return () => clearTimeout(id);
  }, [searchQuery]);

  const getStockStatus = (quantity: number) => {
    if (quantity === 0) return { label: "Out of Stock", variant: "destructive" as const };
    if (quantity < 20) return { label: "Low Stock", variant: "secondary" as const };
    return { label: "In Stock", variant: "default" as const };
  };

  return (
      <div className="space-y-6">
        {/* Header */}
        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
          <div>
            <h1 className="text-2xl font-bold text-foreground">Products</h1>
            <p className="text-muted-foreground">
              Manage your product inventory ({pagedResult?.totalCount || 0} products)
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

        {/* Loading State */}
        {isLoading && (
          <div className="text-center py-12">
            <div className="inline-block h-8 w-8 animate-spin rounded-full border-4 border-solid border-current border-r-transparent align-[-0.125em] motion-reduce:animate-[spin_1.5s_linear_infinite]"></div>
            <p className="mt-4 text-muted-foreground">Loading products...</p>
          </div>
        )}

        {/* Error State */}
        {isError && (
          <div className="text-center py-12">
            <Package className="h-12 w-12 text-destructive mx-auto mb-4" />
            <h3 className="text-lg font-medium text-foreground">Error loading products</h3>
            <p className="text-muted-foreground">{error && "message" in error? error.message : "Failed to fetch products"}</p>
          </div>
        )}

        {/* Products Grid */}
        {!isLoading && !isError && paginatedProducts.length > 0 ? (
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 xl:grid-cols-6 gap-3">
            {paginatedProducts.map((product) => {
              const stockStatus = getStockStatus(product.stock);
              return (
                <Card key={product.id} className="card-hover overflow-hidden">
                  <div className="aspect-[4/3] relative bg-muted">
                    <img
                      src={product.imageUrl}
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
                        Qty: {product.stock}
                      </span>
                    </div>
                  </CardContent>
                </Card>
              );
            })}
          </div>
        ) : null}

        {/* No Results */}
        {!isLoading && !isError && paginatedProducts.length === 0 && (
          <div className="text-center py-12">
            <Package className="h-12 w-12 text-muted-foreground mx-auto mb-4" />
            <h3 className="text-lg font-medium text-foreground">No products found</h3>
            <p className="text-muted-foreground">
              Try adjusting your search query
            </p>
          </div>
        )}

        {/* Pagination */}
        <PagingBar
            currentPage={currentPage}
            itemsPerPage={ITEMS_PER_PAGE}
            totalCount={pagedResult?.totalCount} 
            setCurrentPage={setCurrentPage}  
          />
      </div>
  );
}
