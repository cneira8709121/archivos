using System;
using System.IO;
using System.Text;
using Wintellect.Sterling.Serialization;

namespace Ruv.WPF.Captura.Infrastructure.LocalStorage.Serializadores
{
    /// <summary>
    /// Este serializador encripta los datos de tipo: string.
    /// </summary>
    public class clsSerializador : BaseSerializer
    {
        static StringBuilder SB = new StringBuilder();
        clsCryptoUtil Crypto = RUV.I.Seguridad.Crypto;

        public override bool CanSerialize(Type targetType)
        {
            return targetType.Equals(typeof(string));
        }

        public override void Serialize(object target, BinaryWriter writer)
        {
            var data = Convert.ToString(target);
            writer.Write(Crypto.EncryptStringFixed(data));
        }

        public override object Deserialize(Type type, BinaryReader reader)
        {
            string Texto = reader.ReadString();
            if (string.IsNullOrEmpty(Texto))
                return null;
            else
                return Crypto.DecryptStringFixed(Texto);
        }
    }
}