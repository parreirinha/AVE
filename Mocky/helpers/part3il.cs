//	.locals init(
//        [0] valuetype[mscorlib] System.Collections.Generic.Dictionary`2/Enumerator<object[], object>,
//        [1] valuetype[mscorlib] System.Collections.Generic.KeyValuePair`2<object[], object>,
//        [2] object[],
//        [3] object,
//        [4] bool,
//        [5] int32,
//        [6] bool,
//        [7] bool,
//        [8] bool,
//        [9] object

//    )

//    // (no C# code)
//IL_0000: nop
//    IL_0001: nop
//    // foreach (KeyValuePair<object[], object> result in results)
//    IL_0002: ldarg.0
//	IL_0003: ldfld class [mscorlib] System.Collections.Generic.Dictionary`2<object[], object> Mocky.MockMethod::results
//     IL_0008: callvirt instance valuetype[mscorlib] System.Collections.Generic.Dictionary`2/Enumerator<!0, !1> class [mscorlib] System.Collections.Generic.Dictionary`2<object[], object>::GetEnumerator()
//    // (no C# code)
//IL_000d: stloc.0
//	.try
//	{
//		IL_000e: br.s IL_0077
//        // loop start (head: IL_0077)
//        // foreach (KeyValuePair<object[], object> result in results)
//IL_0010: ldloca.s 0
//			IL_0012: call instance valuetype[mscorlib] System.Collections.Generic.KeyValuePair`2<!0, !1> valuetype[mscorlib] System.Collections.Generic.Dictionary`2/Enumerator<object[], object>::get_Current()
//            // (no C# code)
//            IL_0017: stloc.1

//            IL_0018: nop
//            // object[] key = result.Key;
//            IL_0019: ldloca.s 1

//            IL_001b: call instance !0 valuetype[mscorlib] System.Collections.Generic.KeyValuePair`2<object[], object>::get_Key()
//            IL_0020: stloc.2
//            // object value = result.Value;
//            IL_0021: ldloca.s 1

//            IL_0023: call instance !1 valuetype[mscorlib] System.Collections.Generic.KeyValuePair`2<object[], object>::get_Value()
//            IL_0028: stloc.3
//            // bool flag = true;
//            IL_0029: ldc.i4.1

//            IL_002a: stloc.s 4
//            // for (int i = 0; i < key.Length; i++)
//            IL_002c: ldc.i4.0

//            IL_002d: stloc.s 5
//            // (no C# code)
//            IL_002f: br.s IL_005c
//            // loop start (head: IL_005c)
//                IL_0031: nop
//                // if ((int)key[i] != (int)args[i])
//                IL_0032: ldloc.2

//                IL_0033: ldloc.s 5

//                IL_0035: ldelem.ref
//                IL_0036: unbox.any[mscorlib] System.Int32
//                IL_003b: ldarg.1

//                IL_003c: ldloc.s 5

//                IL_003e: ldelem.ref
//                IL_003f: unbox.any[mscorlib] System.Int32
//                IL_0044: ceq
//                // (no C# code)
//                IL_0046: ldc.i4.0

//                IL_0047: ceq
//                IL_0049: stloc.s 6

//                IL_004b: ldloc.s 6

//                IL_004d: brfalse.s IL_0055


//                IL_004f: nop
//                // flag = false;
//                IL_0050: ldc.i4.0

//                IL_0051: stloc.s 4
//                // (no C# code)
//                IL_0053: br.s IL_0069


//                IL_0055: nop
//                // for (int i = 0; i < key.Length; i++)
//                IL_0056: ldloc.s 5

//                IL_0058: ldc.i4.1

//                IL_0059: add
//                IL_005a: stloc.s 5

//                // for (int i = 0; i < key.Length; i++)
//                IL_005c: ldloc.s 5

//                IL_005e: ldloc.2

//                IL_005f: ldlen
//                IL_0060: conv.i4
//                IL_0061: clt
//                IL_0063: stloc.s 7
//                // (no C# code)
//                IL_0065: ldloc.s 7

//                IL_0067: brtrue.s IL_0031
//            // end loop

//            // if (flag)
//            IL_0069: ldloc.s 4

//            IL_006b: stloc.s 8
//            // (no C# code)
//            IL_006d: ldloc.s 8

//            IL_006f: brfalse.s IL_0076

//            // return value;
//            IL_0071: ldloc.3

//            IL_0072: stloc.s 9
//            // (no C# code)
//            IL_0074: leave.s IL_009b


//            IL_0076: nop

//            // foreach (KeyValuePair<object[], object> result in results)
//            IL_0077: ldloca.s 0

//            IL_0079: call instance bool valuetype [mscorlib]System.Collections.Generic.Dictionary`2/Enumerator<object[], object>::MoveNext()
//            // (no C# code)
//            IL_007e: brtrue.s IL_0010
//        // end loop

//        IL_0080: leave.s IL_0091
//	} // end .try
//	finally
//	{
//		IL_0082: ldloca.s 0
//		IL_0084: constrained.valuetype[mscorlib] System.Collections.Generic.Dictionary`2/Enumerator<object[], object>
//        IL_008a: callvirt instance void[mscorlib] System.IDisposable::Dispose()
//        IL_008f: nop
//        IL_0090: endfinally
//	} // end handler

//	// return 0;
//	IL_0091: ldc.i4.0
//	IL_0092: box[mscorlib] System.Int32

//    IL_0097: stloc.s 9
//    // (no C# code)
//    IL_0099: br.s IL_009b


//    IL_009b: ldloc.s 9

//    IL_009d: ret
//} // end of method MockMethod::Call