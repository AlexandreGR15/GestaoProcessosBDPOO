using GestaoProcessosBDPOO.Dao;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Interfaces;

try
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
}