using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Simple.CommonControl
{
    public class PressEffect : ShaderEffect
    {
        #region Constructors

        static PressEffect()
        {
            _pixelShader.UriSource = Global.MakePackUri("presseffect.ps");
        }

        public PressEffect()
        {
            this.PixelShader = _pixelShader;

            // Update each DependencyProperty that's registered with a shader register.  This
            // is needed to ensure the shader gets sent the proper default value.
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(IsPressedProperty);
        }

        #endregion

        #region Dependency Properties

        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        // Brush-valued properties turn into sampler-property in the shader.
        // This helper sets "ImplicitInput" as the default, meaning the default
        // sampler is whatever the rendering of the element it's being applied to is.
        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(PressEffect), 0);


        public float IsPressed
        {
            get { return (float)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }

        // Scalar-valued properties turn into shader constants with the register
        // number sent into PixelShaderConstantCallback().
        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register("IsPressed", typeof(float), typeof(PressEffect),
                    new UIPropertyMetadata(0f, PixelShaderConstantCallback(0)));

        #endregion

        #region Member Data

        private static PixelShader _pixelShader = new PixelShader();

        #endregion

    }
}
