using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreZapatillas.Data;
using MvcNetCoreZapatillas.Models;

#region SQL SERVER
//VUESTRO PROCEDIMIENTO DE PAGINACION DE IMAGENES DE ZAPATILLAS
#endregion

#region

//alter PROCEDURE SP_IMAGENES_ZAPATILLAS
//(@POSICION INT, @IDPRODUCTO INT)
//AS
//    SELECT IDIMAGEN, IDPRODUCTO, IMAGEN FROM
//        (SELECT CAST(
//            ROW_NUMBER() OVER(ORDER BY IDPRODUCTO) AS INT) AS POSICION,
//            IDIMAGEN, IDPRODUCTO, IMAGEN
//        FROM IMAGENESZAPASPRACTICA
//        WHERE IDPRODUCTO = @IDPRODUCTO) AS QUERY
//    WHERE QUERY.POSICION >= @POSICION AND QUERY.POSICION < (@POSICION + 1)
//GO

#endregion

namespace MvcNetCoreZapatillas.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapatillas()
        {
            return await this.context.Zapatillas.ToListAsync();
        }

        public async Task<Zapatilla> GetZapatilla(int idproducto)
        {
            Zapatilla zapatilla = await this.context.Zapatillas.
                FirstOrDefaultAsync(z => z.IdProducto == idproducto);
            return zapatilla;
        }

        public int GetNumeroZapartillas(int idproducto)
        {
            return this.context.Zapatillas.
                Where(z => z.IdProducto == idproducto).Count();
        }


        /*REVISAR*/
        public async Task<List<ImagenZapatilla>>
        GetImagenesZapatillas(int posicion, int idproducto)
        {
            string sql =
                "SP_IMAGENES_ZAPATILLAS @POSICION, @IDPRODUCTO";
            SqlParameter pamposicion =
                new SqlParameter("@POSICION", posicion);
            SqlParameter pamidproducto =
                new SqlParameter("@IDPRODUCTO", idproducto);

            var consulta =
                this.context.ImagenesZapatillas.FromSqlRaw(sql, pamposicion, pamidproducto);
            List<ImagenZapatilla> imagenes = await consulta.ToListAsync();

            return imagenes;
        }

        //public async Task<List<ImagenZapatilla>>
        //getImagen(int posicion, int idproducto)
        //{
        //    string sql =
        //        "SP_IMAGEN_ZAPATILLAS @POSICION, @IDPRODUCTO";
        //    SqlParameter pamposicion =
        //        new SqlParameter("@POSICION", posicion);
        //    SqlParameter pamidproducto =
        //        new SqlParameter("@IDPRODUCTO", idproducto);

        //    var consulta =
        //        this.context.ImagenesZapatillas.FromSqlRaw(sql, pamposicion, pamidproducto);
        //    List<ImagenZapatilla> imagenes = await consulta.ToListAsync();

        //    return imagenes;
        //}


    }
}
