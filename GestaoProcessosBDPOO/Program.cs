using GestaoProcessosBDPOO.Dao;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Interfaces;

/*try
{
    IServicoDAO servicoDao = new ServicoDAO();
    IUsuarioDao usuarioDao = new UsuarioDAO();
    IClienteDao clienteDao = new ClienteDAO();
    IImovelDao imovelDao = new ImovelDAO();
    IProcessoDao processoDao = new ProcessoDAO();

    // 1) LIST: quantos registros cada tabela trouxe
    Console.WriteLine("Servicos:  " + servicoDao.GetAll().Count);
    Console.WriteLine("Usuarios:  " + usuarioDao.GetAll().Count);
    Console.WriteLine("Clientes:  " + clienteDao.GetAll().Count);
    Console.WriteLine("Imoveis:   " + imovelDao.GetAll().Count);
    Console.WriteLine("Processos: " + processoDao.GetAll().Count);
    

    // 2) BUSCAR ID: busca o registro 1 de cada tabela
    Console.WriteLine("\nBuscarID(1):");
    Console.WriteLine("Servico:  " + servicoDao.BuscarID(1).Nome);
    Console.WriteLine("Usuario:  " + usuarioDao.BuscarID(1).Nome);
    Console.WriteLine("Cliente:  " + clienteDao.BuscarID(1).Email);
    Console.WriteLine("Imovel:   " + imovelDao.BuscarID(1).Denominacao);
    Console.WriteLine("Processo: " + processoDao.BuscarID(1).NumProtocolo);

    
    // 3) INSERT, UPDATE e DELETE (so no Servico, que nao tem FK)
    Console.WriteLine("\nInsert / Update / Delete:");
    Servico novo = new Servico { Nome = "Servico Teste", Ativo = true };
    servicoDao.Insert(novo);
    Console.WriteLine("Inseriu");

    int id = 0;
    foreach (Servico s in servicoDao.GetAll())
        if (s.Nome == "Servico Teste") id = s.ServicoID;

    Servico teste = servicoDao.BuscarID(id);
    teste.Descricao = "Alterado";
    servicoDao.Update(teste);
    Console.WriteLine("Alterou: " + servicoDao.BuscarID(id).Descricao);

    servicoDao.Delete(id);
    Console.WriteLine("Apagou");

    Console.WriteLine("\nTudo funcionando!");
    
}
catch (Exception ex)
{
    Console.WriteLine("ERRO: " + ex.Message);
}*/

try
{
    ClienteImovelDAO vinculoDao = new ClienteImovelDAO();
    MovimentacaoDAO movDao = new MovimentacaoDAO();
    DocumentoDAO docDao = new DocumentoDAO();

    // 1) LIST
    Console.WriteLine("Vinculos:      " + vinculoDao.GetAll().Count);
    Console.WriteLine("Movimentacoes: " + movDao.GetAll().Count);
    Console.WriteLine("Documentos:    " + docDao.GetAll().Count);

    // 2) CLIENTE_IMOVEL: insert, buscar, update, delete (cliente 1 e imovel 1 ja existem)
    vinculoDao.Insert(new ClienteImovel { Percentual = 10, TipoVinculo = "USUFRUTUARIO", ClienteID = 1, ImovelID = 1 });
    int idVinculo = 0;
    foreach (ClienteImovel v in vinculoDao.GetAll())
        if (v.VinculoID > idVinculo) idVinculo = v.VinculoID;      // o ultimo criado
    ClienteImovel vinculo = vinculoDao.BuscarID(idVinculo);
    Console.WriteLine("Vinculo criado: " + vinculo.TipoVinculo + " " + vinculo.Percentual + "%");
    vinculo.Percentual = 25;
    vinculoDao.Update(vinculo);
    Console.WriteLine("Alterado para: " + vinculoDao.BuscarID(idVinculo).Percentual + "%");
    vinculoDao.Delete(idVinculo);
    Console.WriteLine("Vinculo apagado");

    // 3) MOVIMENTACAO (processo 1 e usuario 1 ja existem)
    movDao.Insert(new Movimentacao { DataHora = DateTime.Now, ProcessoID = 1, UsuarioID = 1 });   // Descricao fica NULL
    int idMov = 0;
    foreach (Movimentacao m in movDao.GetAll())
        if (m.MovimentacaoID > idMov) idMov = m.MovimentacaoID;
    Movimentacao mov = movDao.BuscarID(idMov);
    Console.WriteLine("\nMovimentacao criada em " + mov.DataHora + " | Descricao NULL? " + (mov.Descricao == null));
    mov.Descricao = "Teste";
    movDao.Update(mov);
    Console.WriteLine("Alterada: " + movDao.BuscarID(idMov).Descricao);
    movDao.Delete(idMov);
    Console.WriteLine("Movimentacao apagada");

    // 4) DOCUMENTO
    docDao.Insert(new Documento { NomeArquivo = "teste.pdf", TipoDocumento = "TESTE", DataEmissao = new DateOnly(2026, 11, 18), Situacao = "PENDENTE", ProcessoID = 1 });
    int idDoc = 0;
    foreach (Documento d in docDao.GetAll())
        if (d.DocumentoID > idDoc) idDoc = d.DocumentoID;
    Documento doc = docDao.BuscarID(idDoc);
    Console.WriteLine("\nDocumento criado: " + doc.NomeArquivo + " | DataValidade NULL? " + (doc.DataValidade == null));
    doc.DataValidade = new DateOnly(2027, 1, 1);
    doc.Situacao = "RECEBIDO";
    docDao.Update(doc);
    Documento depois = docDao.BuscarID(idDoc);
    Console.WriteLine("Alterado: " + depois.Situacao + " | validade " + depois.DataValidade);
    docDao.Delete(idDoc);
    Console.WriteLine("Documento apagado");

    Console.WriteLine("\nEtapa 9 funcionando!");
}
catch (Exception ex)
{
    Console.WriteLine("ERRO: " + ex.Message);
}