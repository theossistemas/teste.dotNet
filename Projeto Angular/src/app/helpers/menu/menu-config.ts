const adminLteConf = {
  skin: 'blue',
  sidebarLeftMenu: [
    //{label: 'Cadastro', route: '/', iconClasses: 'fa fa-pencil-square-o'}, 
    {
      label: 'Cadastro', iconClasses: 'fa fa-pencil-square-o', children: [
        { label: 'Usuário', route: 'cadastro/usuario' },
        { label: 'Cargo', route: 'cadastro/cargo' },
        { label: 'Perfil', route: 'cadastro/perfil' },
      ]
    },

    { label: 'Histórico', route: '/', iconClasses: 'fa fa-history' },
    { label: 'Justificativa', route: '/justificativa', iconClasses: 'fa fa-th' }
  ]
};

export default adminLteConf;