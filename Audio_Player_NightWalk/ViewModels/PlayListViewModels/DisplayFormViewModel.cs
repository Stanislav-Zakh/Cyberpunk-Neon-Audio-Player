using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_Player_NightWalk
{

    /// <summary>
    /// View Model for the Edit Details Form.
    /// </summary>
    public class DisplayFormViewModel : BaseViewModel
    {

       
        /// <summary>
        /// Default Values
        /// </summary>
        private FullAudiFileInfo _editInfo = new FullAudiFileInfo("Empty", "Empty", 
                                                 "Empty", "Empty", "Empty", "Empty");

        /// <summary>
        /// Indicate that the details where update by the user and need to be saved.
        /// </summary>
        private bool _dirty = false;

        public bool Dirty
        {
            get { return _dirty; }
            set {   
                if (value == _dirty)
                    return;

                _dirty = value; 
                OnPropertyChanged(nameof(Dirty)); 
            }
        }



        /// <summary>
        /// NOTE make dirty a property and bind it to the User Control 
        /// </summary>


        #region Public Properties


        private List<FormRowData> _formRows = new List<FormRowData>();

        public List<FormRowData> FormRows
        {
            get { return _formRows; }
            set { 
                
                _formRows = value;
                OnPropertyChanged(nameof(FormRows));       
            }
        }
        #endregion


        /// <summary>
        /// Load Tag Data to the Form.
        /// </summary>
        /// <param name="focusedtrack"></param>
        public void LoadData(AudioTreeItem focusedtrack)
        {
            // right now we are ignoring PlayList
            if (focusedtrack == null || focusedtrack.GetType() == typeof(PlayListViewModel))
                return;

           _editInfo = TagReader.LoadFullTagInfoAndEdit(focusedtrack.GetFullPath());

            this.FormRows = _editInfo.GetRows();

        }


        /// <summary>
        /// Save new Tag Data to the file (if updated) otherwise return; 
        /// </summary>
        /// <param name="focusedtrack"></param>
        public void SaveData(AudioTreeItem focusedtrack)
        {

            if (!Dirty)
                return;
            //// Note does not update strins in the FormRows before loose focus
            this._editInfo.UpdateData(FormRows);

            TagReader.SaveFullInfo(_editInfo, focusedtrack.GetFullPath());

            focusedtrack.UpdateData(this._editInfo);

            Dirty = false;
        }

        


    }
}
