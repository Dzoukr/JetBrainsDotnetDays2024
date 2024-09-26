module JetBrainsDotnetDays2024.Server.Definition1

open JetBrainsDotnetDays2024.Shared.Definition
open JetBrainsDotnetDays2024.Server.Builders

let private header =
    area {
        name "header"
        direction AreaDirection.Row
        backgroundColor "yellow"
        panels [

        ]
    }

let private main =
    area {
        name "main"
        backgroundColor "blue"
        panels [

        ]
    }

let private sideRight =
    area {
        name "sideRight"
        backgroundColor "fuchsia"
        panels [

        ]
    }


let noPanels =
    definition {
        width (Px 800)
        height (Px 600)
        columns [
            Px 200
            Px 400
            Px 200
        ]
        rows [
            Px 100
            Px 400
            Auto
        ]
        layout [
            "header header header"
            "sideLeft main sideRight"
            "footer footer footer"
        ]
        areas [
            header
            main
            sideRight
        ]
    }