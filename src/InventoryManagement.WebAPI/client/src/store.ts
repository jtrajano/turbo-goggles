import {configureStore} from '@reduxjs/toolkit'
import counterReducer from './features/counterSlice'
import { usersApi } from './services/usersApi';
import { productsApi } from './services/productsApi';

export const store = configureStore({
    reducer:{
        counter: counterReducer,
        [usersApi.reducerPath]: usersApi.reducer,
        [productsApi.reducerPath]: productsApi.reducer
    },
    middleware: (getDefaultMiddleware)=>
        getDefaultMiddleware().concat(usersApi.middleware, productsApi.middleware)
})

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
