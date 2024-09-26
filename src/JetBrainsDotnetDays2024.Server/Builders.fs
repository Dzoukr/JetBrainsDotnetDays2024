module JetBrainsDotnetDays2024.Server.Builders

open System
open JetBrainsDotnetDays2024.Shared.Definition

#nowarn "0025"

module Column =
    let fromWidth (w: Length) = { Width = w }: Column

module Row =
    let fromHeight (h: Length) = { Height = h }: Row

module Layout =
    let fromName (n: string) =
        n.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        |> Array.map (_.Trim())

type CallQueuePanelBuilder() =
    member this.Yield _ =
        {
            PanelDimensions = {
                Width = Auto
                Height = Auto
            }
            MaxCalls = 8
        }
        |> Panel.CallQueuePanel

    [<CustomOperation("width")>]
    member this.Width (CallQueuePanel state, width) =
        { state with CallQueuePanel.PanelDimensions.Width = width }
        |> Panel.CallQueuePanel

    [<CustomOperation("height")>]
    member this.Height (CallQueuePanel state, height) =
        { state with CallQueuePanel.PanelDimensions.Height = height }
        |> Panel.CallQueuePanel

    [<CustomOperation("maxCalls")>]
    member this.MaxCalls (CallQueuePanel state, calls) =
        { state with CallQueuePanel.MaxCalls = calls }
        |> Panel.CallQueuePanel

let callQueuePanel = CallQueuePanelBuilder()

type PlaceholderPanelBuilder() =
    member this.Yield _ =
        {
            PanelDimensions = {
                Width = Auto
                Height = Auto
            }
            BackgroundColor = "#FFFFFFFF"
            Label = ""
        }
        |> Panel.PlaceholderPanel
        : Panel

    [<CustomOperation("width")>]
    member this.Width (PlaceholderPanel state, width) =
       { state with PlaceholderPanel.PanelDimensions.Width = width }
        |> Panel.PlaceholderPanel

    [<CustomOperation("height")>]
    member this.Height (PlaceholderPanel state, height) =
        { state with PlaceholderPanel.PanelDimensions.Height = height }
        |> Panel.PlaceholderPanel

    [<CustomOperation("backgroundColor")>]
    member this.backgroundColor (Panel.PlaceholderPanel state, value) =
        { state with PlaceholderPanel.BackgroundColor = value }
        |> Panel.PlaceholderPanel

    [<CustomOperation("label")>]
    member this.label (Panel.PlaceholderPanel state, value) =
        { state with PlaceholderPanel.Label = value }
        |> Panel.PlaceholderPanel

let placeholderPanel = PlaceholderPanelBuilder()

type ActiveCallPanelBuilder() =
    member this.Yield _ =
        ({
            PanelDimensions = {
                Width = Auto
                Height = Auto
            }
            ActiveCallLabel = ""
        }
        : ActiveCallPanel)
        |> Panel.ActiveCallPanel

    [<CustomOperation("width")>]
    member this.Width (ActiveCallPanel state, width) =
       { state with ActiveCallPanel.PanelDimensions.Width = width }
        |> Panel.ActiveCallPanel

    [<CustomOperation("height")>]
    member this.Height (ActiveCallPanel state, height) =
        { state with ActiveCallPanel.PanelDimensions.Height = height }
        |> Panel.ActiveCallPanel


    [<CustomOperation("activeCallLabel")>]
    member this.ActiveCallLabel (ActiveCallPanel state, value) =
        { state with ActiveCallPanel.ActiveCallLabel = value }
        |> Panel.ActiveCallPanel

let activeCallPanel = ActiveCallPanelBuilder()

type AreaBuilder() =
    member this.Yield _ = {
        Name = ""
        Panels = [||]
        Direction = AreaDirection.Column
        BackgroundColor = ""
    }

    [<CustomOperation("name")>]
    member this.Name (state: Area, value) = { state with Name = value }

    [<CustomOperation("direction")>]
    member this.Direction (state: Area, value) = { state with Direction = value }

    [<CustomOperation("backgroundColor")>]
    member this.backgroundColor (state: Area, value) = { state with BackgroundColor = value }

    [<CustomOperation("panel")>]
    member this.Panel (state: Area, value: Panel) = {
        state with
            Panels = [| value |] |> Array.append state.Panels
    }

    [<CustomOperation("panels")>]
    member this.Panels (state: Area, values: Panel list) = {
        state with
            Panels = values |> List.toArray |> Array.append state.Panels
    }

    member this.Panels (state: Area, values: Panel[]) = {
        state with
            Panels = values |> Array.append state.Panels
    }

let area = AreaBuilder()

type DefinitionBuilder() =
    member this.Yield _ = {
        Width = Length.Auto
        Height = Length.Auto
        Columns = [||]
        Rows = [||]
        Layout = [||]
        Areas = [||]
    }

    [<CustomOperation("width")>]
    member this.Width (state: Definition, width) = { state with Width = width }

    [<CustomOperation("height")>]
    member this.Height (state: Definition, height) = { state with Height = height }

    [<CustomOperation("columns")>]
    member this.Columns (state: Definition, cols: Length seq) = {
        state with
            Columns = cols |> Seq.map Column.fromWidth |> Array.ofSeq
    }

    [<CustomOperation("rows")>]
    member this.Rows (state: Definition, rows: Length seq) = {
        state with
            Rows = rows |> Seq.map Row.fromHeight |> Array.ofSeq
    }

    [<CustomOperation("layout")>]
    member this.Layout (state: Definition, areas: string seq) = {
        state with
            Layout = areas |> Seq.map Layout.fromName |> Array.ofSeq
    }

    [<CustomOperation("areas")>]
    member this.Areas (state: Definition, areas: Area seq) = {
        state with
            Areas = areas |> Array.ofSeq
    }

let definition = DefinitionBuilder()