namespace Fable.Form.Simple.Sutil.Bulma.Fields

open Fable.Form
open Sutil
open Sutil.Bulma
open Fable.Form.Simple.Sutil.Bulma
open Fable.Form.Simple.Fields.Html

module TextField =

    type Field<'Values>(innerField: TextField.InnerField<'Values>) =
        inherit IStandardField<'Values, string, TextField.Attributes>(innerField)

        interface IField<'Values> with

            member _.MapFieldValues(update: 'Values -> 'NewValues) : IField<'NewValues> =

                Field(Field.mapValues update innerField)

        override _.RenderField(config: StandardRenderFieldConfig<string, TextField.Attributes>) =
            InputField.renderField
                {
                    OnChange = config.OnChange
                    OnBlur = config.OnBlur
                    Disabled = config.Disabled
                    IsReadOnly = config.IsReadOnly
                    Value = config.Value
                    Error = config.Error
                    ShowError = config.ShowError
                    Label = config.Attributes.Label
                    FieldId = config.Attributes.FieldId
                    InputFunc = bulma.input.text
                    ExtraInputProps =
                        [
                            match config.Attributes.SpellCheck with
                            | TextField.SpellCheck.Default -> ()
                            | TextField.SpellCheck.True -> prop.spellcheck true
                            | TextField.SpellCheck.False -> prop.spellcheck false

                            prop.autoFocus config.Attributes.AutoFocus

                            match config.Attributes.Placeholder with
                            | Some placeholder -> prop.placeholder placeholder
                            | None -> ()

                            match config.Attributes.AutoComplete with
                            | Some autoComplete -> prop.autoComplete autoComplete
                            | None -> ()
                        ]
                }