using UnityEngine;
using UnityEngine.UI;


public class BoardFactory : MonoBehaviour
{

    [SerializeField] CellFactory cellPrefab;

    const int BOARD_DIMENSION = 10;
    


    public void Create()
    {
        removeCurrentCells();

        //create
        for (int y = 0; y < BOARD_DIMENSION; y++)
        {
            for (int x = 0; x < BOARD_DIMENSION; x++)
            {
                // create the cell
                CellFactory newCell = Instantiate(cellPrefab, transform);

                // position
                newCell.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);

                // scaling
                newCell.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                // naming
                newCell.transform.name = y.ToString() + ',' + x.ToString();
            }
        }
    }

    private void removeCurrentCells()
    {
        CellFactory[] cells = FindObjectsOfType<CellFactory>();

        if (cells.Length > 0)
        {
            foreach (CellFactory cell in cells)
            {
                DestroyImmediate(cell);
            }
        }
    }


}
