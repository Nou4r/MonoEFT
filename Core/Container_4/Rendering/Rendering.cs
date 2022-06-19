using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace eft
{
    public static class Rendering
    {
        private static Texture2D texture;
        public static Material material;
        private static int GL_Mode;
        public static Font font;
        private static int last_gl_mode;
        private static Color BoxColor;

        private static void SetMode(int mode)
        {

        }

        public static void MatrixLine(Vector3 start, Vector3 end, Color color)
        {
            material.SetPass( 0 );
            GL.PushMatrix( );
            GL.LoadProjectionMatrix( Globals.Cam.projectionMatrix );
            GL.modelview = Globals.Cam.worldToCameraMatrix;
            GL.Begin( 1 );
            GL.Color( color );
            GL.Vertex( start );
            GL.Vertex( end );
            GL.End( );
            GL.PopMatrix( );
        }

        public static void Line(Vector2 start, Vector2 end, int thickness, Color color)
        {
            if( !texture )
                texture = new Texture2D( 1 , 1 );

            texture.filterMode = FilterMode.Point;
            var backup_matrix = GUI.matrix;
            var backup_color = GUI.color;
            GUI.color = color;
            var width = end - start;
            float rotate = ( float )( 57.29577951308232 * ( double )Mathf.Atan( width.y / width.x ) );
            if( width.x < 0f )
                rotate += 180f;

            if( thickness < 1 )
                thickness = 1;

            int rotate2 = ( int )Mathf.Ceil( ( float )( thickness / 2 ) );
            GUIUtility.RotateAroundPivot( rotate , start );
            GUI.DrawTexture( new Rect( start.x , start.y - ( float )rotate2 , width.magnitude , ( float )thickness ) , texture );
            GUIUtility.RotateAroundPivot( -rotate , start );
            GUI.color = backup_color;
            GUI.matrix = backup_matrix;
        }

        public static void GlRect(float x, float y, float x1, float y1, Color color)
        {

        }
        public static void Label( float x , float y , float w , float h , string text,  Color color , bool outline, bool center = true )
        {
            var labelStyle = new GUIStyle( );

            labelStyle.font = font;
            labelStyle.fontSize = 9;


            var FontSize = labelStyle.CalcSize( new GUIContent( text ) );

            if( outline )
            {
                labelStyle.normal.textColor = Color.black;
                GUI.Label( new Rect( x , y + 1 , w , h ) , text , labelStyle );
                GUI.Label( new Rect( x , y - 1 , w , h ) , text , labelStyle );
                GUI.Label( new Rect( x - 1 , y , w , h ) , text , labelStyle );
                GUI.Label( new Rect( x + 1 , y , w , h ) , text , labelStyle );
            }

            labelStyle.normal.textColor = color;
            GUI.Label( new Rect( x , y , w , h ) , text , labelStyle );
        }


        public static void RectFilled( float x, float y, float w, float h, Color color )
        {
            if( texture == null )
                texture = new Texture2D( 1, 1 );

            GUI.color = color;
            GUI.DrawTexture( new Rect( x, y, w, h ), texture );
        }
        public static void Rect(float x, float y, float w, float h, Color color)
        {
            Line( new Vector2( x , y ) , new Vector2( x + w , y ) , 1 , color );
            Line( new Vector2( x , y ) , new Vector2( x , y + h ) , 1 , color );
            Line( new Vector2( x + w , y ) , new Vector2( x + w , y + h ) , 1 , color );
            Line( new Vector2( x , y + h ) , new Vector2( x + w , y + h ) , 1 , color );
        }

    }
}
