module JetBrainsDotnetDays2024.Client.Pages.FableExample

open Feliz
open Fable.React
open Feliz.DaisyUI

/// https://github.com/rosskhanas/react-qr-code
[<ReactComponent>]
let private QrCode(code:string) =
    ofImport "QRCode" "react-qr-code" {| value = code  |} []

[<ReactComponent>]
let FableExampleView () =
    let url,setUrl = React.useState("")

    Html.div [
        prop.className "flex flex-col gap-4 m-8"
        prop.children [
            Daisy.input [
                prop.className "w-64"
                prop.type'.text
                prop.placeholder "QR here"
                input.bordered
                prop.onTextChange (fun x -> x |> setUrl)
            ]
            QrCode url
        ]
    ]