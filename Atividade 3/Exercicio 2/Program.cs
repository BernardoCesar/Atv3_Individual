using SkiaSharp;

namespace Projeto {
	class Program {
		static unsafe byte calcularV(byte r, byte g, byte b) {
			// Converte para sRGB (0.0 até 1.0)
			double R = (double)r / 255.0;
			double G = (double)g / 255.0;
			double B = (double)b / 255.0;

			double max = Math.Max(Math.Max(R, G), B);

			double V = max;

			byte v = (byte)(255.0 * V);

			return v;
		}

		static void Main(string[] args) {

			using (SKBitmap bitmap = SKBitmap.Decode("C:\\Users\\bernardo.figueiredo\\Atv3_Individual\\Atividade 3\\Exercicio 2\\Exercicio2.png"), bitmapSaida = new SKBitmap(new SKImageInfo(bitmap.Width, bitmap.Height))) {
				Console.WriteLine(bitmap.ColorType);

				unsafe {

					byte* entrada = (byte*)bitmap.GetPixels();
					byte* saida = (byte*)bitmapSaida.GetPixels();
					int pixelsTotais = bitmap.Width * bitmap.Height;
						for (int e = 0, s = 0; s < pixelsTotais; e += 4, s++) {
							if(calcularV(entrada[e+2], entrada[e+1], entrada[e]) >= 85){
								saida[e + 3] = entrada[e + 3];
								saida[e + 2] = entrada[e + 2];
								saida[e + 1] = entrada[e + 1];
								saida[e] = entrada[e];
							}else{
								saida[e + 3] = 255;
								saida[e + 2] = 0;
								saida[e + 1] = 0;
								saida[e] = 0;
							}
						}
				}

				using (FileStream stream = new FileStream("C:\\Users\\bernardo.figueiredo\\Atv3_Individual\\Atividade 3\\Exercicio 2\\Exercicio2_saida.png", FileMode.OpenOrCreate, FileAccess.Write)) {
					bitmapSaida.Encode(stream, SKEncodedImageFormat.Png, 100);
				}
			}
		}
	}
}
