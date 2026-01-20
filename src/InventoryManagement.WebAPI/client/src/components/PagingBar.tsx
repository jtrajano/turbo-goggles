import { Pagination, PaginationContent, PaginationItem, PaginationLink, PaginationNext, PaginationPrevious } from "./ui/pagination";
import { cn } from "@/lib/utils";



export default function PagingBar(prop 
    :{currentPage: number , totalCount: number, itemsPerPage: number, setCurrentPage  }) {
    
    const currentPage: number = prop.currentPage;
    const itemsPerPage : number = prop.itemsPerPage;
    const totalPage = Math.round(Math.ceil((prop.totalCount || 0) / itemsPerPage));
    const totalCount: number = prop.totalCount;

    const startIndex = (currentPage -1 ) * itemsPerPage;
    const handlePageChange = (page: number)=>{
        if(page >= 1 && page <= totalPage){
          prop.setCurrentPage(page);
        }
    }

    const getPageNumbers =() =>{
        const pages: (number | string)[] = [];
        if(totalPage <= 5) {
            for(let i = 1; i <= totalPage; i++) pages.push(i);
        }
        else {
            if (currentPage <= 3) {
            pages.push(1, 2, 3, 4, "...", totalPage);
            } else if (currentPage >= totalPage - 2) {
            pages.push(1, "...", totalPage - 3, totalPage - 2, totalPage - 1, totalPage);
            } else {
            pages.push(1, "...", currentPage - 1, currentPage, currentPage + 1, "...", totalPage);
            }
        }
        return pages;
    };

    if(!totalPage)
      return <div></div>

    return (
          <div className="flex flex-col sm:flex-row items-center justify-between gap-4 pt-4">
            <p className="text-sm text-muted-foreground">
              Showing {startIndex + 1}-{Math.min(startIndex + itemsPerPage, totalCount || 0)} of{" "}
              {totalCount || 0} products
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
                        className = 
                        {
                            cn(
                                "cursor-pointer",
                                currentPage === page && "bg-primary text-primary-foreground border-primary"
                            )
                        }
                      >
                        {page}
                      </PaginationLink>
                    )}
                  </PaginationItem>
                ))}
                <PaginationItem className="sm:hidden">
                  <span className="px-3 py-2 text-sm">
                    {currentPage} / {totalPage}
                  </span>
                </PaginationItem>
                <PaginationItem>
                  <PaginationNext
                    onClick={() => handlePageChange(currentPage + 1)}
                    className={currentPage === totalPage ? "pointer-events-none opacity-50" : "cursor-pointer"}
                  />
                </PaginationItem>
              </PaginationContent>
            </Pagination>
          </div>
        )


}