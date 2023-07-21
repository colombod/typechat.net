﻿// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.TypeChat.SemanticKernel;

public static class KernelFactory
{
    public static TypeChatJsonTranslator<T> JsonTranslator<T>(Schema schema, ModelInfo model, OpenAIConfig config)
    {
        // Create kernel
        KernelBuilder kb = new KernelBuilder();
        kb.WithChatModel(model.Name, config);
        IKernel kernel = kb.Build();
        // And Json translator
        return kernel.JsonTranslator<T>(schema, model);
    }

    public static TypeChatJsonTranslator<T> JsonTranslator<T>(this IKernel kernel, Schema schema, ModelInfo model)
    {
        TypeChatJsonTranslator<T> typechat = new TypeChatJsonTranslator<T>(
            kernel.CompletionService(model),
            new JsonSerializerTypeValidator<T>(schema)
        );
        return typechat;
    }
}
