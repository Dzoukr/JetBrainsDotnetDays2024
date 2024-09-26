type AddressLine = string

type Person = {
    Name : string
    Age : int option
    Address : string list
}

type PersonBuilder() =
    [<CustomOperation("name")>]
    member this.Name(state,value) = { state with Name = value }

    [<CustomOperation("age")>]
    member this.Age(state,value) = { state with Age = Some value }

    [<CustomOperation("address")>]
    member this.Address(state,value) = { state with Address = value :: state.Address }

    [<CustomOperation("address")>]
    member this.Address(state,value) = { state with Address = value @ state.Address }

    member t.Yield(_) = {
        Name = ""
        Age = None
        Address = []
    }

let person = PersonBuilder()

person {
    name "Roman"

    age 42
    address [
        "Address 1"
        "Address 2"
    ]
}