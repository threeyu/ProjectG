/*
 * Tencent is pleased to support the open source community by making InjectFix available.
 * Copyright (C) 2019 THL A29 Limited, a Tencent company.  All rights reserved.
 * InjectFix is licensed under the MIT License, except for the third-party components listed in the file 'LICENSE' which may be subject to their corresponding license terms. 
 * This file is subject to the terms and conditions defined in file 'LICENSE', which is part of this source code package.
 */

namespace IFix.Core
{
    public enum Code
    {
        Sub_Ovf_Un,
        Shl,
        Ldtoken,
        Stind_I8,
        Ldind_I,
        Stelem_Ref,
        Stind_I,
        Ble_Un,
        Stfld,
        Conv_Ovf_U2,
        Shr,
        Conv_I,
        Conv_Ovf_I,
        Ret,
        Ldarga,
        Tail,
        Or,
        Neg,
        Isinst,
        Stelem_I,
        Ldelem_Ref,
        Ldfld,
        Stelem_I8,
        Brfalse,
        Localloc,
        Dup,
        Stelem_Any,

        //Pseudo instruction
        StackSpace,
        Leave,
        Readonly,
        Add_Ovf,
        And,
        Stind_R8,
        Constrained,
        Conv_Ovf_I8_Un,
        Bne_Un,
        Conv_Ovf_I1,
        Cpobj,
        Ldelem_R8,
        Cpblk,
        Blt,
        Sub_Ovf,
        Call,
        Volatile,
        Stind_R4,
        Ldelem_Any,
        Conv_Ovf_U4_Un,
        CallExtern,
        Ldelem_I2,
        Ldflda,
        Ldtype, // custom
        Castclass,
        Cgt_Un,
        Conv_I8,
        Div_Un,
        Ldind_R8,
        Conv_U1,
        Initblk,
        Ldind_I8,
        Stind_Ref,
        Break,
        Mul_Ovf_Un,
        Conv_R4,
        Rethrow,
        Ldelem_R4,
        Mul,
        Conv_R_Un,
        Endfilter,
        Conv_Ovf_U8,
        Stelem_R4,
        Ldelem_U4,
        Conv_R8,
        Ldind_U1,
        Ldc_R8,
        Ldind_R4,
        Ldsflda,
        Unbox_Any,
        Stind_I4,
        Conv_U4,
        Initobj,
        Conv_Ovf_I2_Un,
        Conv_Ovf_U1_Un,
        Conv_Ovf_U,
        Bgt_Un,
        Ldobj,
        Ldelem_I4,
        Add,
        Callvirtvirt,
        Newanon,
        Pop,
        Ldstr,
        Unaligned,
        Ldvirtftn2,
        Ldind_I4,
        Stelem_I2,
        Clt,
        Brtrue,
        Mul_Ovf,
        Ldelema,
        Ldind_I1,
        Ldelem_I1,
        Ldarg,
        Throw,
        Ldelem_U2,
        Ldftn,
        Switch,
        Stind_I2,
        No,
        Conv_Ovf_U4,
        Conv_U8,
        Newarr,
        Conv_Ovf_U2_Un,
        Nop,
        Stelem_I4,
        Br,
        Endfinally,
        Ldlen,
        Conv_Ovf_I1_Un,
        Conv_Ovf_I4,
        Jmp,
        Refanyval,
        Conv_Ovf_U_Un,
        Xor,
        Bge_Un,
        Starg,
        Cgt,
        Conv_Ovf_I_Un,
        Stelem_I1,
        Not,
        Conv_U2,
        Ldloc,
        Ldind_I2,
        Conv_I4,
        Stelem_R8,
        Stsfld,
        Conv_I1,
        Ldelem_I,
        Ble,
        Beq,
        Conv_Ovf_U8_Un,
        //Calli,
        Ldelem_I8,
        Box,
        Arglist,
        Clt_Un,
        Conv_Ovf_I8,
        Ldc_I4,
        Conv_Ovf_I4_Un,
        Sub,
        Conv_Ovf_U1,
        Conv_I2,
        Ldind_U4,
        Add_Ovf_Un,
        Sizeof,
        Ldind_Ref,
        Ceq,
        Newobj,
        Ldelem_U1,
        Bge,
        Div,
        Ldnull,
        Stobj,
        Bgt,
        Rem,
        Mkrefany,
        Rem_Un,
        Ckfinite,
        Ldc_I8,
        Blt_Un,
        Ldsfld,
        Stind_I1,
        Shr_Un,
        Ldc_R4,
        Conv_Ovf_I2,
        Ldloca,
        Unbox,
        Refanytype,
        Stloc,
        Ldvirtftn,
        Ldind_U2,
        Callvirt,
        Conv_U,
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Instruction
    {
        /// <summary>
        /// 指令MAGIC
        /// </summary>
        public const ulong INSTRUCTION_FORMAT_MAGIC = 4860261748531222922;

        /// <summary>
        /// 当前指令
        /// </summary>
        public Code Code;

        /// <summary>
        /// 操作数
        /// </summary>
        public int Operand;
    }

    public enum ExceptionHandlerType
    {
        Catch = 0,
        Filter = 1,
        Finally = 2,
        Fault = 4
    }

    public sealed class ExceptionHandler
    {
        public System.Type CatchType;
        public int CatchTypeId;
        public int HandlerEnd;
        public int HandlerStart;
        public ExceptionHandlerType HandlerType;
        public int TryEnd;
        public int TryStart;
    }
}