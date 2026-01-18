import {configureStore} from '@reduxjs/toolkit'
import counterReducer from './features/counterSlice'
import { usersApi } from './services/usersApi';

export const store = configureStore({
    reducer:{
        counter: counterReducer,
        [usersApi.reducerPath]: usersApi.reducer
    },
    middleware: (getDefaultMiddleware)=>
        getDefaultMiddleware().concat(usersApi.middleware)
})

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
