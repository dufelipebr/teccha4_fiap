'use client';
import { useRouter } from 'next/navigation';


export  default function Page() 
{
    localStorage.clear();
    const router = useRouter()
    router.back();
}