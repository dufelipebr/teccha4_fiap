'use client';
import React, { Component, useState } from 'react';
import { Label, TextInput, Select } from "flowbite-react";

// export default class InputTeste extends Component {
//     render() 
//     {
//         const { value , onChangeValue } = this.props;
//         return (
//         <div>
//              <TextInput id="num" type="text" sizing="sm" maxLength={16} onChange={onChangeValue} required />
//         </div>
//         );
//     }
// }

export default function InputTeste({onChangeValue}){
    const [val, setVal] = useState("0");
    
    function onClick(){
        setVal(val+1);
        onChangeValue(val);
    }
    
    return(
        <div>
        <TextInput id="num" type="text" sizing="sm" maxLength={16} value={val} onChange={onChangeValue} required />
        <button onClick={onClick} text="Add">Add</button>
        </div>
    );
}
