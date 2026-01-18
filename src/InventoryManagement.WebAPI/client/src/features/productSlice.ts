import { createSlice } from "@reduxjs/toolkit";


const productSlice = createSlice({
    name: 'product',
    initialState: {

    },
    reducers:{
        add: ()=>{

        },
        update:()=>{

        }
    }

});

export const {add, update } = productSlice.actions;
export default productSlice.reducer;