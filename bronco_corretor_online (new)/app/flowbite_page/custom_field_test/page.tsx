'use client';

import InputTeste from "../custom_field/InputTeste";
import { useState } from "react";

export  default function Page() 
{

    //const [valor, setValor] = useState("");

    const handleChange =  (event) => 
    {
        console.log('handleChange');
        console.log(event);
    }

    return(
       <div className="py-20 p-10">
        <InputTeste name="teste"  onChangeValue={handleChange}> </InputTeste>
        </div>
    )
}