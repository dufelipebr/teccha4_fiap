'use client';
import { useState, useEffect } from 'react'
import type { Metadata } from "next";
//import { Inter } from "next/font/google";
import "./globals.css";
import { Dialog, DialogPanel } from '@headlessui/react'
import { Bars3Icon, XMarkIcon } from '@heroicons/react/24/outline'
import {Avatar, Dropdown} from "flowbite-react";
import { LoginInfo } from './lib/login';

//const inter = Inter({ subsets: ["latin"] });
const navigation = [
  
]
/*export const metadata: Metadata = {
  title: "Bronco InsurTec Solutions",
  description: "Somos uma empresa que cuida de você",
};*/

function classNames(...classes) 
{
  return classes.filter(Boolean).join(' ')
}

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {

  const [mobileMenuOpen, setMobileMenuOpen] = useState(false)
  const [isLoggedIn, setLoggedIn] = useState(false)
  const [userInfo, setUserInfo] = useState({nome: '', email: ''})

  useEffect(() => {
    const loginData = LoginInfo();
    if (loginData) {
      setLoggedIn(true)
      setUserInfo({nome: loginData.username, email: loginData.email});
    }
  }, [])

  console.log(userInfo.nome);
  console.log(userInfo.email);

  // const handleLogoff =  (event) => 
  // {

  // }
 
  return (
    <html lang="en">
      <body>
      <div className="bg-orange-300">
      <header className="absolute inset-x-0 top-0 z-50">
        <nav className="flex items-center justify-between p-6 lg:px-8" aria-label="Global">
          <div className="flex lg:flex-1">
            <a href="/" className="-m-1.5 p-1.5">
              <span className="sr-only">Insurtec Solution</span>
              <img
                className="h-8 w-auto"
                src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
                alt=""
              />
            </a>
          </div>
          <div className="flex lg:hidden">
            <button
              type="button"
              className="-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-gray-700"
              onClick={() => setMobileMenuOpen(true)}
            >
              <span className="sr-only">Open main menu</span>
              <Bars3Icon className="h-6 w-6" aria-hidden="true" />
            </button>
          </div>
          <div className="hidden lg:flex lg:gap-x-12">
            {navigation.map((item) => (
              <a key={item.name} href={item.href} className="text-sm font-semibold leading-6 text-gray-900">
                {item.name}
              </a>
            ))}
          </div>
          <div 
            className={classNames(
              !isLoggedIn ? '' : 'hidden',
              'hidden lg:flex lg:flex-1 lg:justify-end',
            )}
          >
            <a href="/login" 
              className={classNames(
                !isLoggedIn ? '' : 'hidden',
                'text-sm font-semibold leading-6 text-gray-900',
              )}
            >
              Entrar  <span aria-hidden="true">&rarr;</span>
            </a>
          </div>

          <div
            className={classNames(
              isLoggedIn ? '' : 'hidden',
              'py-6',
            )}
          >
            {/* <div >
              <Avatar img="/295128.png" alt={"logged in: " + userInfo.nome} rounded />
              <p className="relative rounded-full px-3 py-1 text-sm leading-6 text-gray-600 ring-1 ring-gray-900/10 hover:ring-gray-900/20">{"logged in: " + userInfo.nome} </p>
              
              <p className="px-3 py-1 text-sm leading-6 text-gray-600 text-center"><a href="/logout">Sair</a></p>
            </div> */}
          </div>

        </nav>
        <Dialog className="lg:hidden" open={mobileMenuOpen} onClose={setMobileMenuOpen}>
          <div className="fixed inset-0 z-50" />
          <DialogPanel className="fixed inset-y-0 right-0 z-50 w-full overflow-y-auto bg-white px-6 py-6 sm:max-w-sm sm:ring-1 sm:ring-gray-900/10">
            <div className="flex items-center justify-between">
              <a href="#" className="-m-1.5 p-1.5">
                <span className="sr-only">fi agro</span>
                <img
                  className="h-8 w-auto"
                  src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
                  alt=""
                />
              </a>
              <button
                type="button"
                className="-m-2.5 rounded-md p-2.5 text-gray-700"
                onClick={() => setMobileMenuOpen(false)}
              >
                <span className="sr-only">Close menu</span>
                <XMarkIcon className="h-6 w-6" aria-hidden="true" />
              </button>
            </div>
            <div className="mt-6 flow-root">
              <div className="-my-6 divide-y divide-gray-500/10">
                <div className="space-y-2 py-6">
                  {navigation.map((item) => (
                    <a
                      key={item.name}
                      href={item.href}
                      className="-mx-3 block rounded-lg px-3 py-2 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50"
                    >
                      {item.name}
                    </a>
                  ))}
                </div>
                <div 
                  className={classNames(
                    !isLoggedIn ? '' : 'hidden',
                    'py-6',
                  )}
                >
                  <a
                    href="/login"
                    className="-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50"
                  >
                    Entrar
                  </a>
                </div>
            
              </div>
            </div>
          </DialogPanel>
        </Dialog>
      </header>
        {children}
      </div>
      <div className="bg-indigo-400 py-24 sm:py-32">
      <div className="mx-auto max-w-7xl px-6 lg:px-8">
        <h2 className="text-center text-lg font-semibold leading-3 text-gray-900">
          Trusted by the world’s most innovative teams
        </h2>
        <div className="mx-auto mt-10 grid max-w-lg grid-cols-4 items-center gap-x-8 gap-y-10 sm:max-w-xl sm:grid-cols-6 sm:gap-x-10 lg:mx-0 lg:max-w-none lg:grid-cols-5">
          <img
            className="col-span-2 max-h-12 w-full object-contain lg:col-span-1"
            src="https://tailwindui.com/img/logos/158x48/transistor-logo-gray-900.svg"
            alt="Transistor"
            width={158}
            height={48}
          />
          <img
            className="col-span-2 max-h-12 w-full object-contain lg:col-span-1"
            src="https://tailwindui.com/img/logos/158x48/reform-logo-gray-900.svg"
            alt="Reform"
            width={158}
            height={48}
          />
          <img
            className="col-span-2 max-h-12 w-full object-contain lg:col-span-1"
            src="https://tailwindui.com/img/logos/158x48/tuple-logo-gray-900.svg"
            alt="Tuple"
            width={158}
            height={48}
          />
          <img
            className="col-span-2 max-h-12 w-full object-contain sm:col-start-2 lg:col-span-1"
            src="https://tailwindui.com/img/logos/158x48/savvycal-logo-gray-900.svg"
            alt="SavvyCal"
            width={158}
            height={48}
          />
          <img
            className="col-span-2 col-start-2 max-h-12 w-full object-contain sm:col-start-auto lg:col-span-1"
            src="https://tailwindui.com/img/logos/158x48/statamic-logo-gray-900.svg"
            alt="Statamic"
            width={158}
            height={48}
          />
        </div>
      </div>
    </div>
      </body>
    </html>
  );
}
