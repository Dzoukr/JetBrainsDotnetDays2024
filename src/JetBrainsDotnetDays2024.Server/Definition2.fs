module JetBrainsDotnetDays2024.Server.Definition2

open JetBrainsDotnetDays2024.Shared.Definition
open JetBrainsDotnetDays2024.Server.Builders

let private header =
    area {
        name "header"
        direction AreaDirection.Row
        backgroundColor "pink"
        panels [
            placeholderPanel {
                width (Px 80)
                height (Px 64)
                backgroundColor "#D72622"
                label "PH1"

            }
            placeholderPanel {
                width (Px 80)
                height (Px 64)
                backgroundColor "#D72622"
                label "PH2"
            }
        ]
    }

let private main =
    area {
        name "main"
        backgroundColor "blue"
        panels [
            placeholderPanel {
                width (Px 100)
                height (Px 100)
                backgroundColor "green"
                label "PH Main"
            }
            activeCallPanel {
                activeCallLabel "Calling you..."
                width (Px 100)
                height (Px 100)
            }
        ]
    }

let private sideRight =
    area {
        name "sideRight"
        backgroundColor "fuchsia"
        panels [
            callQueuePanel {
                maxCalls 8
            }
            placeholderPanel {
                width (Px 100)
                height (Px 100)
                backgroundColor "yellow"
                label "PH Right"
            }
        ]
    }


let full =
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