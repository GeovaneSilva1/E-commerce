using iTextSharp.text;
using iTextSharp.text.pdf;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LojaVirtual.ProductApi.DTOs
{
    public class GeracaoPDF
    {
        private readonly IVendaRepository _VendaRepository;
        public string? CNPJ { get; set; }
        public string? RazaoSocial { get; set; }
        public GeracaoPDF(string CNPJ, string RazaoSocial, IVendaRepository vendaRepository)
        {
            this.CNPJ = CNPJ;
            this.RazaoSocial = RazaoSocial;
            _VendaRepository = vendaRepository;
        }

        public byte[] GerarPDF()
        {
            Document document = new Document();
            MemoryStream stream = new MemoryStream();
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
            var query = _VendaRepository.GetQueryRelatorioVendas(this.CNPJ, this.RazaoSocial);


            document.Open();

            Paragraph titulo = RetornaTitulo();
            document.Add(titulo);

            int qtdCampos = 5;

            List<string> listaCampos = new List<string>();
            listaCampos.Add("Id Venda");
            listaCampos.Add("Nome Cliente");
            listaCampos.Add("Produto");
            listaCampos.Add("Quantidade");
            listaCampos.Add("Valor");

            PdfPTable cabecalho = RetornaCabecalho(qtdCampos, listaCampos);
            document.Add(cabecalho);

            List<PdfPTable> tabelas = RetornaDados(qtdCampos,query);

            foreach (var tabela in tabelas)
            {
                document.Add(tabela);
            }

            document.Close();
            pdfWriter.Close();

            return stream.ToArray();
        }

        private static List<PdfPTable> RetornaDados(int qtdCampos, List<VendaRelatorio> query)
        {
            List<PdfPTable> docDados = new List<PdfPTable>();

            foreach (var registro in query) //rodando a query
            {
                PdfPTable linha = new PdfPTable(qtdCampos);
                linha.WidthPercentage = 100;

                PdfPCell idvenda = new PdfPCell(new Phrase(registro.IdVenda.ToString()))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                linha.AddCell(idvenda); //adicionando as colunas

                PdfPCell nomeCliente = new PdfPCell(new Phrase(registro.NomeCliente))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                linha.AddCell(nomeCliente); //adicionando as colunas

                PdfPCell descricaoProduto = new PdfPCell(new Phrase(registro.Produto))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                linha.AddCell(descricaoProduto); //adicionando as colunas

                PdfPCell quantidade = new PdfPCell(new Phrase(registro.Quantidade.ToString()))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                linha.AddCell(quantidade); //adicionando as colunas

                PdfPCell valor = new PdfPCell(new Phrase(Math.Round(registro.Valor,2).ToString()))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                linha.AddCell(valor); //adicionando as colunas


                docDados.Add(linha); //adiciona a linha
            }

            return docDados;
        }

        private static PdfPTable RetornaCabecalho(int qtdColunas, List<string> cabecalhos)
        {
            PdfPTable cabecalhopdf = new PdfPTable(qtdColunas);
            cabecalhopdf.WidthPercentage = 100;
            foreach (var cab in cabecalhos)
            {
                PdfPCell cell = new PdfPCell(new Phrase(cab))
                {
                    BackgroundColor = BaseColor.LIGHT_GRAY,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                cabecalhopdf.AddCell(cell);
            }

            return cabecalhopdf;
        }

        private static Paragraph RetornaTitulo()
        {
            return new Paragraph("Relatório de Vendas")
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20f
            };
        }
    }
}
