let randomList =
    RandomList.generateMutable()
    |> List.map string
    |> String.concat ","

printf $"{randomList}"