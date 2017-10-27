namespace RedWoods.Service
{
    public interface IUserInputService
    {
        /// <summary>
        /// Returns the axis with the associated name and player id.
        ///
        /// It is intended that if the playerid a natural number
        /// (greater than or equal to zero), then the resultant name
        /// of the axis will be appended with an underscore followed
        /// by the playerid.  This is intended to be used as a
        /// convention-based input binding.
        ///
        /// For example, if the axis name is "horz" and the player
        /// id is -1, then the resultant name is "horz".  But if the
        /// player id is 0, then the resultant name is "horz_1".
        ///
        /// In short, use -1 for single player games and 0, 1, 2,
        /// etc. for potentially multiplayer games, assuming the
        /// input axises in Unity are defined with that convention.
        ///
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="playerid"></param>
        /// <returns></returns>
        float GetAxis(string axisName, int playerid = -1);
    }
}