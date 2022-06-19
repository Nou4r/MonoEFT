using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace eft
{

    public class DumbHook
    {
        private const uint HOOK_SIZE_X64 = 12;
        private const uint HOOK_SIZE_X86 = 7;
        private byte [ ] original;

        public MethodInfo OriginalMethod { get; private set; }
        public MethodInfo HookMethod { get; private set; }

        public MethodInfo GetMethodByName( Type typeOrig , string nameOrig )
        {
            return typeOrig.GetMethod( nameOrig );
        }
        public DumbHook( )
        {
            original = null;
            OriginalMethod = HookMethod = null;
        }

        public DumbHook( MethodInfo orig , MethodInfo hook )
        {
            original = null;
            Init( orig , hook );
        }

        public DumbHook( Type typeOrig , string nameOrig , Type typeHook , string nameHook )
        {
            original = null;
            Init( GetMethodByName( typeOrig , nameOrig ) , GetMethodByName( typeHook , nameHook ) );
        }

        public void Init( MethodInfo orig , MethodInfo hook )
        {
            if( orig == null || hook == null )
                throw new ArgumentException( "Both original and hook need to be valid methods" );

            RuntimeHelpers.PrepareMethod( orig.MethodHandle );
            RuntimeHelpers.PrepareMethod( hook.MethodHandle );

            OriginalMethod = orig;
            HookMethod = hook;
        }

        public unsafe void Emplace( )
        {
            if( OriginalMethod == null || HookMethod == null )
                throw new ArgumentException( "no init retard" );
            if( original != null )
                return;

            // Patch it
            IntPtr funcFrom = OriginalMethod.MethodHandle.GetFunctionPointer( );
            IntPtr funcTo = HookMethod.MethodHandle.GetFunctionPointer( );
            uint oldProt;

            if( IntPtr.Size == 8 ) //x86-64
            {
                original = new byte [ HOOK_SIZE_X64 ];

                Import.VirtualProtect( funcFrom , HOOK_SIZE_X64 , 0x40 , out oldProt );

                byte* ptr = ( byte* )funcFrom;

                for( int i = 0 ; i < HOOK_SIZE_X64 ; ++i )
                {
                    original [ i ] = ptr [ i ];
                }

                // movabs rax, addy
                // jmp rax
                *( ptr ) = 0x48;
                *( ptr + 1 ) = 0xb8;
                *( IntPtr* )( ptr + 2 ) = funcTo;
                *( ptr + 10 ) = 0xff;
                *( ptr + 11 ) = 0xe0;

                Import.VirtualProtect( funcFrom , HOOK_SIZE_X64 , oldProt , out oldProt );
            }
        }


        public unsafe void remove( )
        {
            if( original == null )
                return;

            // Restore original code
            uint oldProt;
            uint codeSize = ( uint )original.Length;
            IntPtr origAddr = OriginalMethod.MethodHandle.GetFunctionPointer( );
            Import.VirtualProtect( origAddr , codeSize , 0x40 , out oldProt );
            unsafe
            {
                byte* ptr = ( byte* )origAddr;
                for( var i = 0 ; i < codeSize ; ++i )
                {
                    ptr [ i ] = original [ i ];
                }
            }
            Import.VirtualProtect( origAddr , codeSize , 0x40 , out oldProt );

  
            original = null;
        }


        internal class Import
        {
            [DllImport( "kernel32.dll" , SetLastError = true )]
            internal static extern bool VirtualProtect( IntPtr address , uint size , uint newProtect , out uint oldProtect );
        }

    }

}
