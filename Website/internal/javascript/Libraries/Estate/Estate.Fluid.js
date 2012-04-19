/*
* Summary:
* Adds some fluid behaviour to a page
* 
* Dependencies:
* - Estate.Core
* - JQuery library 1.2.6
*/



Estate.Fluid = {}





/*
 * Summary:
 * FloatListStretch distributes the idle space between all floating items in a list.
 * 
 * Requirements:
 * - FloatListStretch requires a specific structure. You can find that structure below
 * 
 * Usage:
	<head>
		<style type="text/css">
			ul.fluid_FloatListStretch {
				margin: 0px 0px 0px -10px;
				min-height: 1em;
			}
			* html ul.fluid_FloatListStretch {
				height: 1em;
			}

			ul.fluid_FloatListStretch li {
				display: block;
				float: left;
				padding: 0px 0px 10px 10px;
				background: none;
			}

			ul.fluid_FloatListStretch li span {
				display: block;
				width: 50px;
				height: 50px;
				background: #eee;
			}
		</style>

		<script type="text/javascript">
			jQuery(document).ready( function() { Estate.Fluid.FloatListStretch.AddListID( 'Fluid_FloatListStretch' )   // Repeat this line to ad another element
												 Estate.Fluid.FloatListStretch.Init()
											   } )	
			Estate.Events.AddEvent ( window,
									 function() { Estate.Fluid.FloatListStretch.Reflow()   // Redistribute the idle space after a window resize },
									 "onresize"
								   )
		</script>
	</head>

	<body>
		<ul id="Fluid_FloatListStretch" class="fluid_FloatListStretch">
			<li>
				<span></span>
			</li>
		</ul>
	</body>
 */

Estate.Fluid.FloatListStretch = (function() {
	/* START PRIVATE */
	var listIDs = new Array();
	var listItemPaddingInPixels = new Array();
	var listItemPerRow = new Array();
	var listContentBoxWidthInPixels = new Array();
	var listBoxWidthInPixels = new Array();
	var unusedPixelsInListBox = new Array();



	function GetCSSValueAsNumber( jqSelector, Style ) {
		var elementValue = jQuery( jqSelector ).css( Style )
		
		if ( elementValue.indexOf( '.' ) > 0 ) {
			throw new Error( "A style value of the element with the selector '" + jqSelector + "' is a float, but it should be a round number. Make sure the values are measured in 'px'" );
		}
		
		return parseInt( Estate.StringTools.RemoveMeasurement( elementValue ) )
	}

	function GetUnusedPixelsInListBox( listIndex ) {
		var i = 0
		var usedPixelsInListBox = 0
		var totalListBoxWidth = 0
		
		while( usedPixelsInListBox < listBoxWidthInPixels[listIndex] ) {
			totalListBoxWidth = listContentBoxWidthInPixels[listIndex]
			if ( i > 0 ) {
				totalListBoxWidth += listItemPaddingInPixels[listIndex]
			}
			if ( ( usedPixelsInListBox + totalListBoxWidth ) >= listBoxWidthInPixels[listIndex] ) {
				listItemPerRow[listIndex] = i
				unusedPixelsInListBox[listIndex] = (listBoxWidthInPixels[listIndex] - usedPixelsInListBox) - listItemPaddingInPixels[listIndex]
				return true
			}
			usedPixelsInListBox += totalListBoxWidth
			i++
		}
	}

	function DistributeUnusedPixels( listIndex ) {
		var extraPadding = Math.floor( unusedPixelsInListBox[listIndex] / ( listItemPerRow[listIndex] - 1 ) )
		var extraPaddingLeftover = unusedPixelsInListBox[listIndex] - ( extraPadding * ( listItemPerRow[listIndex] - 1 ) )
		var ListItemsCollection = document.getElementById( listIDs[listIndex] ).getElementsByTagName('li')
		
		var newPadding
		for ( var i=0; i < ListItemsCollection.length; i++ ) {
			if ( i % listItemPerRow[listIndex] != 0 ) {
				newPadding = extraPadding
				if ( distributeExtraPaddingLeftover > 0 ) {
					newPadding++
					distributeExtraPaddingLeftover--
				}
				ListItemsCollection[i].style.paddingLeft = ( newPadding + listItemPaddingInPixels[listIndex] ) +"px"
			} else {
				distributeExtraPaddingLeftover = extraPaddingLeftover
			}
		}
	}

	function ResetList( listIndex ) {
		var ListItemsCollection = document.getElementById( listIDs[listIndex] ).getElementsByTagName('li')

		for ( var i=0; i < ListItemsCollection.length; i++ ) {
			ListItemsCollection[i].style.paddingLeft = listItemPaddingInPixels[listIndex] +"px"
		}
	}
	/* END PRIVATE */



	/* START PUBLIC */
	return {
		AddListID: function( ListId ) {
			var error
			error = Estate.Check.ArgumentsCount( arguments.length, 1 );
			if ( error != "" ) throw new Error( error );
			error = Estate.Check.VariableType( ListId, "string" );
			if ( error != "" ) throw new Error( error );
			error = Estate.Check.ElementById( ListId );
			if ( error != "" ) throw new Error( error );
			
			listIDs.push( ListId )
		},

		Init: function() {
			var error
			error = Estate.Check.ArgumentsCount( arguments.length, 0 );
			if ( error != "" ) throw new Error( error );
			
			for ( var i=0; i < listIDs.length; i++ ) {
				listItemPaddingInPixels[i] = GetCSSValueAsNumber( '#' + listIDs[i] + ' li:first-child', 'padding-left' )
				listContentBoxWidthInPixels[i] = GetCSSValueAsNumber( '#' + listIDs[i] + ' li:first-child span:first-child', 'width' )
				listBoxWidthInPixels[i] = jQuery( '#' + listIDs[i] ).width()
				GetUnusedPixelsInListBox( i )
								
				DistributeUnusedPixels( i )
			}
		},

		Reflow: function() {
			var error
			error = Estate.Check.ArgumentsCount( arguments.length, 0 );
			if ( error != "" ) throw new Error( error );
			
			for ( var i=0; i < listIDs.length; i++ ) {
				ResetList( i )

				listBoxWidthInPixels[i] = jQuery( '#' + listIDs[i] ).width()
				GetUnusedPixelsInListBox( i )
								
				DistributeUnusedPixels( i )
			}
		}
	};
	/* END PUBLIC */
})();
