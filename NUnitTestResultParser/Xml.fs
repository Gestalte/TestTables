module Xml

open System.Xml.Linq

    let LoadXml (filePath:string) =
        XElement.Load(filePath)
