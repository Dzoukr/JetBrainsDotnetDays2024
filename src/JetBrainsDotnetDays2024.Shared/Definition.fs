module JetBrainsDotnetDays2024.Shared.Definition

type Length =
    | Auto
    | Px of int
    | Max

type AreaDirection =
    | Row
    | Column

type Column = { Width: Length }

type Row = { Height: Length }

type PanelDimensions = {
    Width: Length
    Height: Length
}

type PlaceholderPanel = {
    PanelDimensions: PanelDimensions
    BackgroundColor: string
    Label: string
}

type CallQueuePanel = {
    PanelDimensions: PanelDimensions
    MaxCalls: int
}

type ActiveCallPanel = {
    PanelDimensions: PanelDimensions
    ActiveCallLabel: string
}

type Panel =
    | ActiveCallPanel of ActiveCallPanel
    | PlaceholderPanel of PlaceholderPanel
    | CallQueuePanel of CallQueuePanel

type Area = {
    Name: string
    Panels: Panel[]
    Direction: AreaDirection
    BackgroundColor: string
}

type Definition = {
    Width: Length
    Height: Length
    Columns: Column[]
    Rows: Row[]
    Layout: string[][]
    Areas: Area[]
}