module JetBrainsDotnetDays2024.Client.Renderers

open Feliz
open JetBrainsDotnetDays2024.Shared.Definition

[<RequireQualifiedAccess>]
module Length =
    let toICssUnit (l: Length) =
        match l with
        | Auto -> length.auto
        | Px px -> length.px px
        | Max -> length.percent 100

let private getStyles (pd:PanelDimensions) =
    [
        style.width (Length.toICssUnit pd.Width)
        style.height (Length.toICssUnit pd.Height)
    ]

[<ReactComponent>]
let private RenderPlaceholderPanel (p:PlaceholderPanel) =
    Html.div [
        prop.style [
            yield! (getStyles p.PanelDimensions)
            style.backgroundColor p.BackgroundColor
        ]
        prop.text p.Label
    ]


[<ReactComponent>]
let private RenderActiveCallPanel (p:ActiveCallPanel) =
    Html.div [
        prop.style [
            yield! (getStyles p.PanelDimensions)
        ]
        prop.text p.ActiveCallLabel
    ]

[<ReactComponent>]
let private RenderCallQueuePanel (p:CallQueuePanel) =
    Html.div [
        prop.style [
            yield! (getStyles p.PanelDimensions)
        ]
        prop.children [
            yield! [1..p.MaxCalls] |> List.map (fun x ->
                Html.div $"Call #{x}"
            )
        ]
    ]

[<ReactComponent>]
let private RenderPanel (panel:Panel) =
    match panel with
    | PlaceholderPanel ph -> RenderPlaceholderPanel ph
    | ActiveCallPanel activeCallPanel -> RenderActiveCallPanel activeCallPanel
    | CallQueuePanel callQueuePanel -> RenderCallQueuePanel callQueuePanel

[<ReactComponent>]
let private RenderArea (area: Area) =
    Html.div [
        prop.classes [
            "flex"
            match area.Direction with
            | AreaDirection.Row -> "flex-row"
            | AreaDirection.Column -> "flex-col"
        ]
        prop.style [
            style.gridArea area.Name
            style.overflow.hidden
            style.gap 2
            style.backgroundColor area.BackgroundColor
        ]
        prop.children (area.Panels |> Seq.map RenderPanel)
    ]

[<ReactComponent>]
let RenderDefinition (definition:Definition) =
    Html.div [
        prop.style [
            style.display.grid
            style.backgroundColor "#8C8C8C"
            style.width (Length.toICssUnit definition.Width)
            style.height (Length.toICssUnit definition.Height)
            style.gridTemplateColumns (
                definition.Columns
                |> Array.map (fun x -> x.Width)
                |> Array.map Length.toICssUnit
            )
            style.gridTemplateRows (
                definition.Rows
                |> Array.map (fun x -> x.Height)
                |> Array.map Length.toICssUnit
            )
            style.gridTemplateAreas (
                definition.Layout |> Array.map (Array.map id)
            )
        ]
        prop.children (definition.Areas |> Seq.map RenderArea)
    ]