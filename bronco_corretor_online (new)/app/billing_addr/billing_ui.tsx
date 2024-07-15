'use client';
import { useState } from "react";
import { Label, TextInput, Select } from "flowbite-react";

function classNames(...classes) 
{
  return classes.filter(Boolean).join(' ')
}
interface PaymentInfo{
    payType: string, 
    num: string,
    name: string,
    cvv: string,
    exp: string,
    errorNumber: int
}

export function PayMethod({onChangeValue}) {

    const [enableCC, setEnableCC] = useState(false);
    const [payment, setPayment] = useState<PaymentInfo>({payType:"B", num:"", name:"", cvv:"",exp:""});
    const [errorNumber, setErrorNumber] = useState(0);
    //const [completed, setCompleted] = useState(true);

    const handleCombo = (event) =>
    {
        //event.preventDefault();
        console.log("internal.billing_ui.handleCombo");
        console.log(event.target.name);
        console.log(event.target.value);
       
        payment.payType = event.target.value;
        if(payment.payType == "D" || payment.payType == "C")
            setEnableCC(true);
        else
            setEnableCC(false);

        console.log("enableCC");
        console.log(enableCC);
        handleError();
        onChangeValue(payment);
    }

    const handleChange =  (event) => 
    {
        //event.preventDefault();
        if (event.target.name == "num")
        {
            payment.num = event.target.value;
        }

        if (event.target.name == "cvv")
        {
           payment.cvv = event.target.value;

        }

        if (event.target.name == "name")
        {
           payment.name = event.target.value;
        }

        if (event.target.name == "exp")
        {
           payment.exp = event.target.value;
        }

        handleError();
        onChangeValue(payment);
    }

    const handleError =  () => 
    {
        const NUM_CARD = /\d{16}/;
        const CVV = /\d{3}/;
        const EXP = /^(0[1-9]|1[0-2])\/?([0-9]{2})$/;

        console.log("handleError");
        console.log("payment.payType");
        console.log(payment.payType);

        payment.errorNumber = 0;
        var validar = (payment.payType == "D" || payment.payType == "C");
        if (validar && !NUM_CARD.test(payment.num))
            payment.errorNumber += 1;

        if (validar && payment.name.trim().length < 5)
            payment.errorNumber += 2;

        if (validar && !CVV.test(payment.cvv))
            payment.errorNumber += 4;

        if (validar && !EXP.test(payment.exp))
            payment.errorNumber += 8;

        setErrorNumber(payment.errorNumber);
        console.log(errorNumber);
    }

    
  
  return (
    <div className="grid grid-cols-2 gap-4">
        <div className="max-w-md col-span-2">
            <div className="mb-2 block">
            <Label htmlFor="methodtype" value="Selecione a forma de pagamento" />
            </div>
            <Select id="methodtype" name="methodtype" onChange={handleCombo} required>
            <option value="B">Boleto</option>
            <option value="D">Cartão Debito</option>
            <option value="C">Cartão de Crédito</option>
            <option value="P">Pix</option>
            </Select>
        </div>
        <div 
            className={classNames(
                enableCC ? '' : 'hidden',
                'max-w-md col-span-2',
            )}
        >
            <div className="mb-2 block">
                <Label htmlFor="small" value="Informe o numero do cartão" 
                    className={classNames(
                        errorNumber & 1 ? 'text-red-500 font-bold' : '',
                        'max-w-md col-span-2',
                    )}
                />
            </div>
            <TextInput id="num" name="num" placeholder="preencha o número" type="text" sizing="sm" maxLength={16} onChange={handleChange} required />
        </div>
        <div 
            className={classNames(
                enableCC ? '' : 'hidden',
                'max-w-md col-span-2',
            )}
         >
            <div className="mb-2 block">
                <Label htmlFor="small" value="Nome" 
                    className={classNames(
                        errorNumber & 2 ? 'text-red-500 font-bold' : '',
                        'max-w-md col-span-2',
                    )}
                />
            </div>
            <TextInput id="name" name="name" placeholder="preencha o nome" type="text" sizing="sm" maxLength={40} onChange={handleChange} required />
        </div>
        <div
           className={classNames(
            enableCC ? '' : 'hidden',
            'mb-2 block',
            )}
        >
            <div className="mb-2 block">
                <Label htmlFor="small" value="CVV" 
                    className={classNames(
                        errorNumber & 4 ? 'text-red-500 font-bold' : '',
                        'max-w-md col-span-2',
                    )}
                />
            </div>
            <TextInput id="cvv" name="cvv" placeholder="000" type="password" sizing="sm" maxLength={3} onChange={handleChange} required />
        </div>
        <div
          className={classNames(
            enableCC ? '' : 'hidden',
            'mb-2 block',
            )}
        >
            <div className="mb-2 block">
                <Label htmlFor="small" value="Expira em" 
                    className={classNames(
                        errorNumber & 8 ? 'text-red-500 font-bold' : '',
                        'max-w-md col-span-2',
                    )}
                />
            </div>
            <TextInput id="exp" name="exp" type="text" sizing="sm"   placeholder="mm/aa"  maxLength={5} onChange={handleChange} required />
        </div>
        
    </div>
  );
}
