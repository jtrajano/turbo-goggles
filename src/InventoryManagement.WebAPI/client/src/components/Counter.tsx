import { increment, decrement, incrementByValue } from "@/features/counterSlice";
import { useAppDispatch, useAppSelector } from "@/hooks/useRedux";
import { Plus } from "lucide-react";
import { Button } from "@/components/ui/button";


function Counter() {
    const count = useAppSelector((state) => state.counter.value);
    const dispatch = useAppDispatch();

    return (
        <div>
            <h1> Count: {count}</h1>
            <Button onClick={()=> dispatch( increment())}>Increment</Button>
            <Button onClick={()=> dispatch( decrement())}>Decrement</Button>
            <Button onClick={()=> dispatch( incrementByValue(2))}>Increment by 2</Button>
        </div>
    )
    
}   
export default Counter;