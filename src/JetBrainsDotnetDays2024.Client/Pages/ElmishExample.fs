module JetBrainsDotnetDays2024.Client.Pages.ElmishExample

open Feliz
open Feliz.DaisyUI
open Elmish
open Feliz.UseElmish

type private State = {
    Count : int
}

type private Msg =
    | Increase
    | Decrease

let private init () =
    { Count = 0 }, Cmd.none

let private update (msg:Msg) (state:State) : State * Cmd<Msg> =
    match msg with
    | Increase ->
        if state.Count > 15 then
            { state with Count = 0 }, Cmd.none
        else
            { state with Count = state.Count + 1 }, Cmd.none
    | Decrease -> { state with Count = state.Count - 1 }, Cmd.none

[<ReactComponent>]
let private ElmishPowered() =
    let state, dispatch = React.useElmish(init, update, [| |])

    Html.div [
        Html.h1 [ prop.className "text-3xl"; prop.text "useElmish" ]
        Daisy.button.div [
            prop.text "Increase"
            prop.onClick (fun _ -> Increase |> dispatch)

        ]
        Daisy.button.div [
            prop.text "Decrease"
            prop.onClick (fun _ -> Decrease |> dispatch)
        ]
        Html.div [
            prop.className "p-4 text-5xl"
            prop.text state.Count
        ]
    ]

let private UseStatePowered() =
    let value,setValue = React.useState(0)

    // werid logic

    Html.div [
        Html.h1 [ prop.className "text-3xl"; prop.text "useState" ]

        Daisy.button.div [
            prop.text "Increase"
            prop.onClick (fun _ -> value + 1 |> setValue)

        ]
        Daisy.button.div [
            prop.text "Decrease"
            prop.onClick (fun _ -> value - 1 |> setValue)
        ]
        Html.div [
            prop.className "p-4 text-5xl"
            prop.text value
        ]
    ]

[<ReactComponent>]
let ElmishExampleView () =
    Html.div [
        prop.className "p-8 flex flex-col gap-8"
        prop.children [
            UseStatePowered()
            ElmishPowered()
        ]
    ]