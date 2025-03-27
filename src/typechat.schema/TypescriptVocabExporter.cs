﻿// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.TypeChat.Schema;

/// <summary>
/// This class is reserved for TypeChat infrastructure
/// Exports vocabularies as schema using Typescript
/// Some vocabularies are exported inline, but others may be exported as standalone types
/// </summary>
public class TypescriptVocabExporter : TypeExporter<NamedVocab>
{
    private readonly TypescriptWriter _writer;

    public TypescriptVocabExporter(TypescriptWriter writer, IVocabCollection store)
        : base()
    {
        ArgumentVerify.ThrowIfNull(writer, nameof(writer));
        ArgumentVerify.ThrowIfNull(store, nameof(store));

        _writer = writer;
        this.Vocabs = store;
    }

    public IVocabCollection Vocabs { get; }

    public override void ExportType(NamedVocab type)
    {
        ArgumentVerify.ThrowIfNull(type, nameof(type));

        if (IsExported(type))
        {
            return;
        }

        _writer.SOL();
        _writer.Type(type.Name).Space().Assign().Space();
        {
            ExportValues(type.Vocab);
        }
        _writer.EOS();
        AddExported(type);
    }

    private void ExportValues(IVocab vocab)
    {
        var values = from entry in vocab
                     select entry.Text;
        _writer.Literals(values);
    }
}
